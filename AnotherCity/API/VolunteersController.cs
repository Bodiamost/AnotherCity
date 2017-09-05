using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AnotherCity.Data;
using AnotherCity.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AnotherCity.API
{
    [Produces("application/json")]
    [Route("api/Volunteers")]
    public class VolunteersController : Controller
    {
        private readonly AnotherCityDbContext _context;

        public VolunteersController(AnotherCityDbContext context)
        {
            _context = context;
        }

        // GET: api/Volunteers
        [HttpGet]
        public IEnumerable<Volunteer> GetVolunteers()
        {
            return _context.Volunteers;
        }
        // GET: api/Volunteers/GetVolunteerForm/1
        [HttpGet]
        [Route("GetVolunteerForm/{id}")]
        public IActionResult GetVolunteerForm([FromRoute] int id)
        {
            ViewData["OpportunityId"] = new SelectList(_context.VolunteerOpportunities, "Id", "Title",id);
            return PartialView("_Create");
        }
        // GET: api/Volunteers//PostVolunteerForm
        [HttpPost]
        [Route("PostVolunteerForm")]
        public async Task<IActionResult> PostVolunteerForm([FromBody] Volunteer volunteer)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_Create",volunteer);

                //return BadRequest(ModelState);
            }

            volunteer.ApplicationDate = DateTime.Now;
            _context.Volunteers.Add(volunteer);
            await _context.SaveChangesAsync();

            return Json("OK");
        }
        // GET: api/Volunteers/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVolunteer([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var volunteer = await _context.Volunteers.SingleOrDefaultAsync(m => m.Id == id);

            if (volunteer == null)
            {
                return NotFound();
            }

            return Ok(volunteer);
        }

        // PUT: api/Volunteers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVolunteer([FromRoute] int id, [FromBody] Volunteer volunteer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != volunteer.Id)
            {
                return BadRequest();
            }

            _context.Entry(volunteer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VolunteerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Volunteers
        [HttpPost]
        public async Task<IActionResult> PostVolunteer([FromBody] Volunteer volunteer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Volunteers.Add(volunteer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVolunteer", new { id = volunteer.Id }, volunteer);
        }

        // DELETE: api/Volunteers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVolunteer([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var volunteer = await _context.Volunteers.SingleOrDefaultAsync(m => m.Id == id);
            if (volunteer == null)
            {
                return NotFound();
            }

            _context.Volunteers.Remove(volunteer);
            await _context.SaveChangesAsync();

            return Ok(volunteer);
        }

        private bool VolunteerExists(int id)
        {
            return _context.Volunteers.Any(e => e.Id == id);
        }
    }
}