using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AnotherCity.Data;
using AnotherCity.Models;
using AnotherCity.Utilities;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace AnotherCity.Controllers
{
    
    public class ProjectsController : Controller
    {
        private readonly AnotherCityDbContext _context;
        private readonly IStringLocalizer<ProjectsController> _localizer;

        public ProjectsController(AnotherCityDbContext context,IStringLocalizer<ProjectsController> localizer)
        {
            _context = context;    
            _localizer = localizer;
        }
        
        
        // GET: Projects
        [Authorize]
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["TitleSortParm"] = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "date" ? "date_desc" : "date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var projects = from s in _context.Projects.Include(p => p.Member)
                             select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                projects = projects.Where(s => s.Title.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "title":
                    projects = projects.OrderBy(s => s.Title);
                    break;
                case "title_desc":
                    projects = projects.OrderByDescending(s => s.Title);
                    break;
                case "date":
                    projects = projects.OrderBy(s => s.StartDate);
                    break;
                default:
                    projects = projects.OrderByDescending(s => s.StartDate);
                    break;
            }
            // var anotherCityDbContext = _context.Volunteers.Include(v => v.Account).Include(v => v.VolunteerOpportunity);
            int pageSize = 10;
            return View(await PaginatedList<Project>.CreateAsync(projects.AsNoTracking(), page ?? 1, pageSize));
        }

        // GET: Projects/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .Include(p => p.Member)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // GET: Projects/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["MemberId"] = new SelectList(_context.Members, "Id", "LastName");
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ICollection<IFormFile> images,IFormFile MainImg,[Bind("Id,Featured,MemberId,Title,VolunteerOpp,InvestOpp,StartDate,FinishDate,ShortDesc,Description,Location,LatLng,SocialLinks,Status")] Project project)
        {
            if (ModelState.IsValid)
            {
                // Image upload --- rewrite with good structure
                if (MainImg != null)
                {
                    long size = MainImg.Length;
                    // full path to file in temp location
                    var filePath = Path.Combine("wwwroot/uploads/", MainImg.FileName);
                    if (MainImg.Length > 0)
                    {
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await MainImg.CopyToAsync(stream);
                        }
                    }

                    //Database save
                    project.MainImg = MainImg.FileName;
                }
                
                _context.Add(project);
                await _context.SaveChangesAsync();
                
                if(project.VolunteerOpp)
                {
                    VolunteerOpportunity task = new VolunteerOpportunity();
                    task.JobType = "Help";
                    task.ProjectId = project.Id;
                    task.Title = project.Title + " hepl!";
                    _context.Add(task);
                    await _context.SaveChangesAsync();
                }

                if (images != null)
                {

                    foreach (var img in images)
                    {
                        var filePath = Path.Combine("wwwroot/uploads/", img.FileName);

                        if (img.Length > 0)
                        {
                            using (var fileStream = new FileStream(filePath, FileMode.Create))
                            {
                                await img.CopyToAsync(fileStream);
                                Image tmpImg = new Image();
                                tmpImg.Name = "Image";
                                tmpImg.Path = img.FileName;
                                tmpImg.ProjectId = project.Id;
                                _context.Images.Add(tmpImg);
                                await _context.SaveChangesAsync();

                            }
                        }

                    }
                }
                return RedirectToAction("Index");
            }
            ViewData["MemberId"] = new SelectList(_context.Members, "Id", "LastName", project.MemberId);
            return View(project);
        }

        // GET: Projects/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .Include(i => i.Images)
                .Include(m => m.ProjectSocials)
                    .ThenInclude(ms => ms.Social)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (project == null)
            {
                return NotFound();
            }
            ViewData["MemberId"] = new SelectList(_context.Members, "Id", "LastName", project.MemberId);
            ViewData["SocialId"] = new SelectList(_context.SocialServices, "Id", "Title");
            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ICollection<IFormFile> images, IFormFile MainImg, int id, [Bind("Id,Featured,MemberId,Title,VolunteerOpp,InvestOpp,StartDate,FinishDate,ShortDesc,Description,Location,LatLng,SocialLinks,Status")] Project project)
        {
            if (id != project.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Image upload --- rewrite with good structure
                    if(MainImg!=null)
                    { 
                        long size = MainImg.Length;
                        // full path to file in temp location
                        var filePath = Path.Combine("wwwroot/uploads/", MainImg.FileName);
                        if (MainImg.Length > 0)
                        {
                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                await MainImg.CopyToAsync(stream);
                            }
                        }

                        //Database save
                        project.MainImg = MainImg.FileName;
                    }
                    else
                    {
                        project.MainImg = (await _context.Projects.AsNoTracking().SingleOrDefaultAsync(m => m.Id == id)).MainImg;
                    }
                    _context.Update(project);
                    await _context.SaveChangesAsync();
                  
                    if (project.VolunteerOpp && _context.VolunteerOpportunities.Any(opp => opp.ProjectId == project.Id))
                    {
                        VolunteerOpportunity task = new VolunteerOpportunity();
                        task.JobType = "Help";
                        task.ProjectId = project.Id;
                        task.Title = project.Title+" hepl!";
                        _context.Add(task);
                        await _context.SaveChangesAsync();
                    }

                    if (images != null)
                    {

                        foreach (var img in images)
                        {
                            var filePath = Path.Combine("wwwroot/uploads/", img.FileName);

                            if (img.Length > 0)
                            {
                                using (var fileStream = new FileStream(filePath, FileMode.Create))
                                {
                                    await img.CopyToAsync(fileStream);
                                    Image tmpImg = new Image();
                                    tmpImg.Name = "Image";
                                    tmpImg.Path = img.FileName;
                                    tmpImg.ProjectId = id;
                                    _context.Images.Add(tmpImg);
                                    await _context.SaveChangesAsync();

                                }
                            }
                            
                        }           
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectExists(project.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            ViewData["MemberId"] = new SelectList(_context.Members, "Id", "LastName", project.MemberId);
            ViewData["SocialId"] = new SelectList(_context.SocialServices, "Id", "Title");
            return View(project);
        }

        // GET: Projects/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .Include(p => p.Member)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var project = await _context.Projects.SingleOrDefaultAsync(m => m.Id == id);
            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ProjectExists(int id)
        {
            return _context.Projects.Any(e => e.Id == id);
        }

        [Route("")]
        public async Task<IActionResult> IndexAll()
        {
            var anotherCityDbContext = _context.Projects.Where(p => p.MainImg != null)
                .Include(p => p.Member);
            return View(await anotherCityDbContext.ToListAsync());
        }

        [Route("/past")]
        public async Task<IActionResult> PastProjects()
        {
            var anotherCityDbContext = _context.Projects.Where(p => p.MainImg != null)
                .Include(p => p.Member)
                .Where(d => ((d.StartDate.Value.Month - DateTime.Now.Month) + 12 * (d.StartDate.Value.Year - DateTime.Now.Year)) < -1)
                .OrderByDescending(s => s.StartDate);
            
            ViewData["Title"] = _localizer["Projects_Past"];
            return View("IndexSome", await anotherCityDbContext.ToListAsync());
        }

        [Route("/active")]
        public async Task<IActionResult> CurrentProjects()
        {
            var anotherCityDbContext = _context.Projects.Where(p => p.MainImg != null)
                .Include(p => p.Member)
                .Where(d => Math.Abs(((d.StartDate.Value.Month - DateTime.Now.Month) + 12 * (d.StartDate.Value.Year - DateTime.Now.Year))) <=1)
                .OrderByDescending(s => s.FinishDate);
            
            ViewData["Title"] = _localizer["Projects_Current"];
            return View("IndexSome", await anotherCityDbContext.ToListAsync());
        }

        [Route("/future")]
        public async Task<IActionResult> FutureProjects()
        {
            var anotherCityDbContext = _context.Projects.Where(p => p.MainImg != null)
                .Include(p => p.Member)
                .Where(d => ((d.StartDate.Value.Month - DateTime.Now.Month) + 12 * (d.StartDate.Value.Year - DateTime.Now.Year))>1)
                .OrderBy(s => s.StartDate);

            ViewData["Title"] = _localizer["Projects_Future"];
            return View("IndexSome", await anotherCityDbContext.ToListAsync());
        }

        [Route("/project/{id}")]
        public async Task<IActionResult> ViewProject(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .Include(p => p.Member)
                .Include(p => p.Images)
                .Include(p => p.ProjectSocials)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }
    }
}
