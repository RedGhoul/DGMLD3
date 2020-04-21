using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DGMLD3.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using DGMLD3.Models;

namespace DGMLD3.Controllers
{
    [Authorize]
    public class LinksController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public LinksController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Links
        public async Task<IActionResult> Index(string currentGraphId, string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            //Do some user auth

            var graph = await _context.Graphs.Where(x => x.Id == Int32.Parse(currentGraphId)).FirstOrDefaultAsync();
            ViewBag.GraphName = graph.Name;
            ViewData["CurrentGraphId"] = currentGraphId;
            ViewData["TargetSortParm"] = String.IsNullOrEmpty(sortOrder) ? "target_desc" : "target_asc";
            ViewData["SourceSortParm"] = String.IsNullOrEmpty(sortOrder) ? "source_desc" : "source_asc";
          
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["SearchFilter"] = searchString;

            var links = from s in _context.Links where s.GraphId == Int32.Parse(currentGraphId) select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                links = links.Where(s => s.source.Contains(searchString)
                                       || s.target.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "target_desc":
                    links = links.OrderByDescending(s => s.target);
                    break;
                case "target_asc":
                    links = links.OrderBy(s => s.target);
                    break;
                case "source_desc":
                    links = links.OrderByDescending(s => s.source);
                    break;
                case "source_asc":
                    links = links.OrderBy(s => s.source);
                    break;
                default:
                    links = links.OrderByDescending(s => s.target);
                    break;
            }
            int pageSize = 10;
            return View(await PaginatedList<Link>.CreateAsync(links.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        [HttpPost]
        public async Task<IActionResult> Index(string currentGraphId, string searchString)
        {
            //Do some user auth

            var graph = await _context.Graphs.Where(x => x.Id == Int32.Parse(currentGraphId)).FirstOrDefaultAsync();
            ViewBag.GraphName = graph.Name;

            ViewData["CurrentGraphId"] = currentGraphId;
            var links = from s in _context.Links where s.GraphId == Int32.Parse(currentGraphId) select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                links = links.Where(s => s.source.Contains(searchString)
                                       || s.target.Contains(searchString));
            }

            ViewBag.PlaceHolderSearch = searchString;
            int pageSize = 10;
            return View(await PaginatedList<Link>.CreateAsync(links.AsNoTracking(),1, pageSize));
        }


        // GET: Links/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var link = await _context.Links
                .Include(l => l.Graph)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (link == null)
            {
                return NotFound();
            }

            return View(link);
        }

        // GET: Links/Create
        public IActionResult Create()
        {
            ViewData["GraphId"] = new SelectList(_context.Graphs, "Id", "Id");
            return View();
        }

        // POST: Links/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,source,target,GraphId")] Link link)
        {
            if (ModelState.IsValid)
            {
                _context.Add(link);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GraphId"] = new SelectList(_context.Graphs, "Id", "Id", link.GraphId);
            return View(link);
        }

        // GET: Links/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var link = await _context.Links.FindAsync(id);
            if (link == null)
            {
                return NotFound();
            }
            ViewData["GraphId"] = new SelectList(_context.Graphs, "Id", "Id", link.GraphId);
            return View(link);
        }

        // POST: Links/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,source,target,GraphId")] Link link)
        {
            if (id != link.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(link);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LinkExists(link.Id))
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
            ViewData["GraphId"] = new SelectList(_context.Graphs, "Id", "Id", link.GraphId);
            return View(link);
        }

        // GET: Links/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var link = await _context.Links
                .Include(l => l.Graph)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (link == null)
            {
                return NotFound();
            }

            return View(link);
        }

        // POST: Links/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var link = await _context.Links.FindAsync(id);
            _context.Links.Remove(link);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LinkExists(int id)
        {
            return _context.Links.Any(e => e.Id == id);
        }
    }
}
