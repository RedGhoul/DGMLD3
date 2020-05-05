using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DGMLD3.Data.CONTEXT;
using DGMLD3.Data.RDMS;

namespace DGMLD3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SVGStoreController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SVGStoreController(ApplicationDbContext context)
        {
            _context = context;
        }
        public class SVGDTO
        {
            public string SVG { get; set; }
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
                    return Ok();
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
