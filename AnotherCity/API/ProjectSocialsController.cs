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
    [Route("api/ProjectSocials")]
    public class ProjectSocialsController : Controller
    {
        private readonly AnotherCityDbContext _context;

        public ProjectSocialsController(AnotherCityDbContext context)
        {
            _context = context;
        }

        // GET: api/ProjectSocials
        [HttpGet]
        public IEnumerable<ProjectSocials> GetProjectsSocials()
        {
            return _context.ProjectsSocials;
        }

        // GET: api/ProjectSocials/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProjectSocials([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var projectSocials = await _context.ProjectsSocials.SingleOrDefaultAsync(m => m.Id == id);

            if (projectSocials == null)
            {
                return NotFound();
            }

            return Ok(projectSocials);
        }

        // PUT: api/ProjectSocials/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProjectSocials([FromRoute] int id, [FromBody] ProjectSocials projectSocials)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != projectSocials.Id)
            {
                return BadRequest();
            }

            _context.Entry(projectSocials).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectSocialsExists(id))
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
        // GET: api/ProjectSocials/GetProjectSocialsForm/1
        [HttpGet]
        [Route("GetProjectSocialsForm/{id}")]
        public IActionResult GetProjectSocialsForm([FromRoute] int id)
        {
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Title",id);
            ViewData["SocialId"] = new SelectList(_context.SocialServices.Where(ss => !_context.ProjectsSocials.Where(ps=> ps.ProjectId==id && ss.Id == ps.SocialId ).Any()), "Id", "Title");
            return PartialView("_Add");
        }
        // POST: api/ProjectSocials
        [HttpPost]
        public async Task<IActionResult> PostProjectSocials([FromBody] ProjectSocials projectSocials)
        {
            if (!ModelState.IsValid || _context.ProjectsSocials.SingleOrDefault(ms => ms.ProjectId == projectSocials.ProjectId && ms.SocialId == projectSocials.SocialId) != null)
            {
                return BadRequest(ModelState);
            }

            _context.ProjectsSocials.Add(projectSocials);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProjectSocials", new { id = projectSocials.Id }, projectSocials);
        }

        // DELETE: api/ProjectSocials/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProjectSocials([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var projectSocials = await _context.ProjectsSocials.SingleOrDefaultAsync(m => m.Id == id);
            if (projectSocials == null)
            {
                return NotFound();
            }

            _context.ProjectsSocials.Remove(projectSocials);
            await _context.SaveChangesAsync();

            return Ok(projectSocials);
        }

        private bool ProjectSocialsExists(int id)
        {
            return _context.ProjectsSocials.Any(e => e.Id == id);
        }
    }
}