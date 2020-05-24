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
        public async Task<IActionResult> Index(int? pageNumber)
        {
            GraphTableViewModel view = new GraphTableViewModel();

            var user = await _userManager.GetUserAsync(HttpContext.User);
            var graphs = from s in _context.Graphs where s.Creator.Id.Equals(user.Id) select s;

            int pageSize = 10;
            view.PageList = PaginatedList<Graph>.Create(graphs.AsNoTracking(), pageNumber ?? 1, pageSize);
            return View(view);
        }

        public async Task<IActionResult> Sort(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            GraphTableViewModel view = new GraphTableViewModel();
            if (!String.IsNullOrEmpty(sortOrder))
            {
                view.NameSortParm = sortOrder.Equals("name_desc") ? "name_asc" : "name_desc";
                view.GraphNameSortParm = sortOrder.Equals("g_name_desc") ? "g_name_asc" : "g_name_desc";
                view.DateSortParm = sortOrder.Equals("date_desc") ? "date_asc" : "date_desc";
                view.IsPublicParm = sortOrder.Equals("ispublic_desc") ? "ispublic_asc" : "ispublic_desc";
            }
           

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            view.SearchString = searchString;
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var graphs = from s in _context.Graphs where s.Creator.Id.Equals(user.Id) select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                graphs = graphs.Where(s => s.Name.Contains(searchString)
                                       || s.ReadableName.Contains(searchString));
            }

            graphs = sortOrder switch
            {
                "name_desc" => graphs.OrderByDescending(s => s.Name),
                "name_asc" => graphs.OrderBy(s => s.Name),
                "g_name_desc" => graphs.OrderByDescending(s => s.ReadableName),
                "g_name_asc" => graphs.OrderBy(s => s.ReadableName),
                "date_desc" => graphs.OrderByDescending(s => s.DateCreated),
                "date_asc" => graphs.OrderBy(s => s.DateCreated),
                "ispublic_desc" => graphs.OrderBy(s => s.IsPublic),
                "ispublic_asc" => graphs.OrderByDescending(s => s.IsPublic),
                _ => graphs.OrderBy(s => s.Name),
            };
            int pageSize = 10;
            view.PageList = PaginatedList<Graph>.Create(graphs.AsNoTracking(), pageNumber ?? 1, pageSize);
            return View("Index",view);
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
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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
    }
}
