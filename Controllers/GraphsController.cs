using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DGMLD3.Data;
using Microsoft.AspNetCore.Authorization;
using DGMLD3.Models;

namespace DGMLD3.Controllers
{
    [Authorize(Roles = "Admin")]
    public class GraphsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GraphsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Graphs
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "name_asc";
            ViewData["GraphNameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "g_name_desc" : "g_name_asc";
            ViewData["DateSortParm"] = String.IsNullOrEmpty(sortOrder) ? "date_desc" : "date_asc";
            
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["SearchFilter"] = searchString;

            var graphs = from s in _context.Graphs select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                graphs = graphs.Where(s => s.Name.Contains(searchString)
                                       || s.ReadableName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    graphs = graphs.OrderByDescending(s => s.Name);
                    break;
                case "name_asc":
                    graphs = graphs.OrderBy(s => s.Name);
                    break;
                case "g_name_desc":
                    graphs = graphs.OrderByDescending(s => s.ReadableName);
                    break;
                case "g_name_asc":
                    graphs = graphs.OrderBy(s => s.ReadableName);
                    break;
                case "date_desc":
                    graphs = graphs.OrderByDescending(s => s.DateCreated);
                    break;
                case "date_asc":
                    graphs = graphs.OrderByDescending(s => s.DateCreated);
                    break;
                default:
                    graphs = graphs.OrderBy(s => s.Name);
                    break;
            }
            int pageSize = 10;
            return View(await PaginatedList<Graph>.CreateAsync(graphs.AsNoTracking(), pageNumber ?? 1, pageSize));
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

        // GET: Graphs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Graphs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Graph graph)
        {
            if (ModelState.IsValid)
            {
                _context.Add(graph);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Graph graph)
        {
            if (id != graph.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(graph);
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
