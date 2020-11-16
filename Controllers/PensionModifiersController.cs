using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PF.Data;
using PF.Data.Models;

namespace PF.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PensionModifiersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PensionModifiersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/PensionModifiers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PensionModifier>>> GetDisabilityGroups()
        {
            return await _context.DisabilityGroups.ToListAsync();
        }

        // GET: api/PensionModifiers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PensionModifier>> GetPensionModifier(Guid id)
        {
            var pensionModifier = await _context.DisabilityGroups.FindAsync(id);

            if (pensionModifier == null)
            {
                return NotFound();
            }

            return pensionModifier;
        }

        // PUT: api/PensionModifiers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPensionModifier(Guid id, PensionModifier pensionModifier)
        {
            if (id != pensionModifier.Id)
            {
                return BadRequest();
            }

            _context.Entry(pensionModifier).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PensionModifierExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }

        // POST: api/PensionModifiers
        [HttpPost]
        public async Task<ActionResult<PensionModifier>> PostPensionModifier(PensionModifier pensionModifier)
        {
            _context.DisabilityGroups.Add(pensionModifier);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPensionModifier", new { id = pensionModifier.Id }, pensionModifier);
        }

        // DELETE: api/PensionModifiers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PensionModifier>> DeletePensionModifier(Guid id)
        {
            var pensionModifier = await _context.DisabilityGroups.FindAsync(id);
            if (pensionModifier == null)
            {
                return NotFound();
            }

            _context.DisabilityGroups.Remove(pensionModifier);
            await _context.SaveChangesAsync();

            return pensionModifier;
        }

        private bool PensionModifierExists(Guid id)
        {
            return _context.DisabilityGroups.Any(e => e.Id == id);
        }
    }
}
