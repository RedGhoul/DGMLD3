using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DGMLD3.Data.CONTEXT;
using DGMLD3.Data.RDMS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using DGMLD3.Data.DTO;

namespace DGMLD3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SVGStoreController : BaseController
    {

        public SVGStoreController(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager) : base(context, userManager)
        {
        }


        // POST: api/SVGStore/5
        [HttpPost("SaveSVG/{id}")]
        public async Task<IActionResult> SaveSVG(int id, SVGDTO SVG)
        {

            if (!GraphExists(id))
            {
                return NotFound();
            }

            var graph = _context.Graphs.FirstOrDefault(x => x.Id == id);

            if (!string.IsNullOrEmpty(SVG.SVG))
            {
                graph.SVG = SVG.SVG;

                try
                {
                    await _context.SaveChangesAsync();
                    return Ok("Saved SVG");
                }
                catch (DbUpdateConcurrencyException error)
                {
                    Console.WriteLine(error.InnerException);

                }
            }

            return BadRequest();

        }

        private bool GraphExists(int id)
        {
            return _context.Graphs.Any(e => e.Id == id);
        }
    }
}
