using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AnotherCity.Data;
using AnotherCity.Models;
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
        public async Task<IActionResult> Index()
        {
            var anotherCityDbContext = _context.Volunteers.Include(v => v.Account).Include(v => v.VolunteerOpportunity);
            return View(await anotherCityDbContext.ToListAsync());
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
                _context.Add(volunteer);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index","Home");
            }
            ViewData["OpportunityId"] = new SelectList(_context.VolunteerOpportunities, "Id", "Title",volunteer.VolunteerOpportunityId);
            return View(volunteer);
        }
    }
}
