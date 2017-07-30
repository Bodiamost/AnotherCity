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
using Microsoft.AspNetCore.Authorization;

namespace AnotherCity.Controllers
{
    public class VolunteersController : Controller
    {
        private readonly AnotherCityDbContext _context;

        public VolunteersController(AnotherCityDbContext context)
        {
            _context = context;    
        }

        // GET: Volunteers

        [Authorize]
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["OppSortParm"] = sortOrder == "opp" ? "opp_desc" : "opp";
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

            var volunteers = from s in _context.Volunteers.Include(v => v.Account).Include(v => v.VolunteerOpportunity)
                             select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                volunteers = volunteers.Where(s => s.LastName.Contains(searchString)
                                       || s.FirstName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name":
                    volunteers = volunteers.OrderBy(s => s.LastName);
                    break;
                case "name_desc":
                    volunteers = volunteers.OrderByDescending(s => s.LastName);
                    break;
                case "opp":
                    volunteers = volunteers.OrderBy(s => s.VolunteerOpportunity.Title);
                    break;
                case "opp_desc":
                    volunteers = volunteers.OrderByDescending(s => s.VolunteerOpportunity.Title);
                    break;
                case "date":
                    volunteers = volunteers.OrderBy(s => s.ApplicationDate);
                    break;
                default:
                    volunteers = volunteers.OrderByDescending(s => s.ApplicationDate);
                    break;
            }
            // var anotherCityDbContext = _context.Volunteers.Include(v => v.Account).Include(v => v.VolunteerOpportunity);
            int pageSize = 10;
            return View(await PaginatedList<Volunteer>.CreateAsync(volunteers.AsNoTracking(), page ?? 1, pageSize));
        }

        // GET: Volunteers/Details/5

        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var volunteer = await _context.Volunteers
                .Include(v => v.Account)
                .Include(v => v.VolunteerOpportunity)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (volunteer == null)
            {
                return NotFound();
            }

            return View(volunteer);
        }



        // GET: Volunteers/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var volunteer = await _context.Volunteers
                .Include(v => v.Account)
                .Include(v => v.VolunteerOpportunity)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (volunteer == null)
            {
                return NotFound();
            }

            return View(volunteer);
        }

        // POST: Volunteers/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var volunteer = await _context.Volunteers.SingleOrDefaultAsync(m => m.Id == id);
            _context.Volunteers.Remove(volunteer);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool VolunteerExists(int id)
        {
            return _context.Volunteers.Any(e => e.Id == id);
        }

        // GET: Volunteers/Create
        public IActionResult Create(int id=0)
        {
            ViewData["OpportunityId"] = new SelectList(_context.VolunteerOpportunities, "Id", "Title",id);

            return View();
        }

        // POST: Volunteers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Bio,SocialLink,VolunteerOpportunityId,FirstName,LastName,Email,Phone")] Volunteer volunteer)
        {
            if (ModelState.IsValid)
            {
                volunteer.ApplicationDate = DateTime.Now; 
                _context.Add(volunteer);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index","Home");
            }
            ViewData["OpportunityId"] = new SelectList(_context.VolunteerOpportunities, "Id", "Title",volunteer.VolunteerOpportunityId);
            return View(volunteer);
        }
    }
}
