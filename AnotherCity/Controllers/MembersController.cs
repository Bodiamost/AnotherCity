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
using Microsoft.AspNetCore.Http;
using System.IO;

namespace AnotherCity.Controllers
{
    public class MembersController : Controller
    {
        private readonly AnotherCityDbContext _context;

        public MembersController(AnotherCityDbContext context)
        {
            _context = context;    
        }

        // GET: Members
        [Authorize(Roles = "SuperUser")]
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["PosSortParm"] = sortOrder == "pos" ? "pos_desc" : "pos";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var members = from s in _context.Members.Include(m => m.Account).Include(m => m.Position)
            select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                members = members.Where(s => s.LastName.Contains(searchString)
                                       || s.FirstName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    members = members.OrderByDescending(s => s.LastName);
                    break;
                case "pos":
                    members = members.OrderBy(s => s.Position.Title);
                    break;
                case "pos_desc":
                    members = members.OrderByDescending(s => s.Position.Title);
                    break;
                default:
                    members = members.OrderBy(s => s.LastName);
                    break;
            }
            // var anotherCityDbContext = _context.Volunteers.Include(v => v.Account).Include(v => v.VolunteerOpportunity);
            int pageSize = 10;
            return View(await PaginatedList<Member>.CreateAsync(members.AsNoTracking(), page ?? 1, pageSize));
        }

        // GET: Members/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await _context.Members
                .Include(m => m.Account)
                .Include(m => m.Position)
                .Include(m => m.MemberSocials)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        // GET: Members/Create
        [Authorize(Policy = "AddEditUser")]
        public IActionResult Create()
        {
            ViewData["PositionId"] = new SelectList(_context.Positions, "Id", "Title");

            return View();
        }

        // POST: Members/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Policy = "AddEditUser")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile PhotoImg, [Bind("PositionId,Bio,TopMember,Id,FirstName,LastName,Email,Phone")] Member member)
        {
            if (ModelState.IsValid)
            {
                // Image upload --- rewrite with good structure
                if (PhotoImg != null)
                {
                    long size = PhotoImg.Length;
                    // full path to file in temp location
                    var filePath = Path.Combine("wwwroot/uploads/profileimgs/", PhotoImg.FileName);
                    if (PhotoImg.Length > 0)
                    {
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await PhotoImg.CopyToAsync(stream);
                        }
                    }

                    //Database save
                    member.PhotoImg = PhotoImg.FileName;
                }
                _context.Add(member);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["PositionId"] = new SelectList(_context.Positions, "Id", "Title", member.PositionId);

            return View(member);
        }

        // GET: Members/Edit/5
        [Authorize(Policy = "AddEditUser")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await _context.Members
                .Include(m => m.MemberSocials)
                    .ThenInclude(ms => ms.Social)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (member == null)
            {
                return NotFound();
            }
            ViewData["PositionId"] = new SelectList(_context.Positions, "Id", "Title", member.PositionId);
            ViewData["SocialId"] = new SelectList(_context.SocialServices, "Id", "Title");
            return View(member);
        }

        // POST: Members/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Policy = "AddEditUser")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(IFormFile PhotoImg, int id, [Bind("PositionId,Bio,TopMember,Id,FirstName,LastName,Email,Phone")] Member member)
        {
            if (id != member.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Image upload --- rewrite with good structure
                    if (PhotoImg != null && PhotoImg.Length != 0)
                    {
                        long size = PhotoImg.Length;
                        // full path to file in temp location
                        var filePath = Path.Combine("wwwroot/uploads/profileimgs/", PhotoImg.FileName);
                        if (PhotoImg.Length > 0)
                        {
                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                await PhotoImg.CopyToAsync(stream);
                            }
                        }

                        //Database save
                        member.PhotoImg = PhotoImg.FileName;
                    }
                    else
                    {
                        member.PhotoImg = (_context.Members.AsNoTracking().SingleOrDefault(m => m.Id == id)).PhotoImg;
                    }
                    member.AccountId = (_context.Members.AsNoTracking().SingleOrDefault(m => m.Id == id)).AccountId;
                    _context.Update(member);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MemberExists(member.Id))
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
            ViewData["PositionId"] = new SelectList(_context.Positions, "Id", "Title", member.PositionId);
            ViewData["SocialId"] = new SelectList(_context.SocialServices, "Id", "Title");
            return View(member);
        }

        // GET: Members/Delete/5
        [Authorize(Roles = "Admin")]
        [Authorize(Policy = "DeleteUser")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await _context.Members
                .Include(m => m.Account)
                .Include(m => m.Position)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var member = await _context.Members.SingleOrDefaultAsync(m => m.Id == id);
            _context.Members.Remove(member);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool MemberExists(int id)
        {
            return _context.Members.Any(e => e.Id == id);
        }

        public async Task<IActionResult> IndexAll()
        {
            var anotherCityDbContext = _context.Members
                                       .Include(m => m.Account)
                                       .Include(m => m.Position)
                                       .Include(m => m.MemberSocials);
            return View(await anotherCityDbContext.ToListAsync());
        }
    }
}
