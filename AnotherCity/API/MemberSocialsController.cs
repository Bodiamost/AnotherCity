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
    [Route("api/MemberSocials")]
    public class MemberSocialsController : Controller
    {
        private readonly AnotherCityDbContext _context;

        public MemberSocialsController(AnotherCityDbContext context)
        {
            _context = context;
        }

        // GET: api/MemberSocials
        [HttpGet]
        public IEnumerable<MemberSocials> GetMembersSocials()
        {
            return _context.MembersSocials;
        }

        // GET: api/MemberSocials/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMemberSocials([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var memberSocials = await _context.MembersSocials.SingleOrDefaultAsync(m => m.Id == id);

            if (memberSocials == null)
            {
                return NotFound();
            }

            return Ok(memberSocials);
        }

        // PUT: api/MemberSocials/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMemberSocials([FromRoute] int id, [FromBody] MemberSocials memberSocials)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != memberSocials.Id)
            {
                return BadRequest();
            }

            _context.Entry(memberSocials).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MemberSocialsExists(id))
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
        // GET: api/MemberSocials/GetMemberSocialsForm/1
        [HttpGet]
        [Route("GetMemberSocialsForm/{id}")]
        public IActionResult GetMemberSocialsForm([FromRoute] int id)
        {
            ViewData["MemberId"] = new SelectList(_context.Projects, "Id", "Title", id);
            ViewData["SocialId"] = new SelectList(_context.SocialServices.Where(ss => !_context.MembersSocials.Where(ms => ms.MemberId == id && ss.Id == ms.SocialId).Any()), "Id", "Title");
            return PartialView("_Add");
        }

        // POST: api/MemberSocials
        [HttpPost]
        public async Task<IActionResult> PostMemberSocials([FromBody] MemberSocials memberSocials)
        {
            if (!ModelState.IsValid || _context.MembersSocials.SingleOrDefault(ms => ms.MemberId==memberSocials.MemberId && ms.SocialId==memberSocials.SocialId)!=null)
            {
                return BadRequest(ModelState);
            }

            _context.MembersSocials.Add(memberSocials);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMemberSocials", new { id = memberSocials.Id }, memberSocials);
        }

        // DELETE: api/MemberSocials/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMemberSocials([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var memberSocials = await _context.MembersSocials.SingleOrDefaultAsync(m => m.Id == id);
            if (memberSocials == null)
            {
                return NotFound();
            }

            _context.MembersSocials.Remove(memberSocials);
            await _context.SaveChangesAsync();

            return Ok(memberSocials);
        }

        private bool MemberSocialsExists(int id)
        {
            return _context.MembersSocials.Any(e => e.Id == id);
        }
    }
}