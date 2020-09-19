using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using DGMLD3.Data.CONTEXT;
using DGMLD3.Data.RDMS;
using DGMLD3.Data.VIEW;
using Microsoft.AspNetCore.Identity;

namespace DGMLD3.Controllers
{
    [Authorize]
    public class GraphsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public GraphsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Graphs
        public async Task<IActionResult> Index()
        {
            GraphTableViewModel vm = new GraphTableViewModel();
            ApplicationUser user = await GetCurrentUser();
            vm.Graphs = await _context.Graphs.Where(s => s.Creator.Id.Equals(user.Id)).ToListAsync();
            return View(vm);
        }

        // GET: Graphs
        public async Task<IActionResult> Search([FromQuery] string SearchString)
        {
            GraphTableViewModel vm = new GraphTableViewModel();
            ApplicationUser user = await GetCurrentUser();
            vm.Graphs = await _context.Graphs.Where(s => s.Creator.Id.Equals(user.Id)).ToListAsync();
            return View(vm);
        }



        // GET: Graphs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var graph = await _context.Graphs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (graph == null)
            {
                return NotFound();
            }

            return View(graph);
        }

        // GET: Graphs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var graph = await _context.Graphs.FindAsync(id);
            if (graph == null)
            {
                return NotFound();
            }
            return View(graph);
        }

        // POST: Graphs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ReadableName,IsPublic")] Graph graph)
        {
            if (id != graph.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var curGraph = await _context.Graphs.Where(x => x.Id == graph.Id).FirstOrDefaultAsync();
                    curGraph.ReadableName = graph.ReadableName;
                    curGraph.IsPublic = graph.IsPublic;
                    //_context.Update(graph);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GraphExists(graph.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(graph);
        }

        // GET: Graphs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var graph = await _context.Graphs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (graph == null)
            {
                return NotFound();
            }

            return View(graph);
        }

        // POST: Graphs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var graph = await _context.Graphs.FindAsync(id);
            _context.Graphs.Remove(graph);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GraphExists(int id)
        {
            return _context.Graphs.Any(e => e.Id == id);
        }

        private async Task<ApplicationUser> GetCurrentUser()
        {
            return await _userManager.GetUserAsync(HttpContext.User);
        }
    }
}
