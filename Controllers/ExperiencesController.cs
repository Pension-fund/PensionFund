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
    public class ExperiencesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ExperiencesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // PUT: api/Experiences
        [HttpPut]
        public async Task<IActionResult> PutExperience(Experience experience)
        {
            _context.Entry(experience).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok();
        }

        // POST: api/Experiences
        [HttpPost]
        public async Task<ActionResult<Experience>> PostExperience(Experience experience)
        {
            _context.Experiences.Add(experience);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
