using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DGMLD3.Data;
using Microsoft.AspNetCore.Authorization;
using DGMLD3.Data.CONTEXT;
using DGMLD3.Data.RDMS;

namespace DGMLD3.Controllers
{
    [Authorize(Roles ="Admin")]
    public class PricingController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PricingController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Pricing
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var plans = await _context.PricePlans.Where(x => x.IsActive == true).OrderBy(x => x.Id).ToListAsync();
            return View(plans);
        }

        // GET: Pricing/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pricing = await _context.PricePlans
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pricing == null)
            {
                return NotFound();
            }

            return View(pricing);
        }

        // GET: Pricing/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pricing/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,ChargeAmount,Features,BillingPer,IsActive")] PricePlan pricing)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pricing);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pricing);
        }

        // GET: Pricing/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pricing = await _context.PricePlans.FindAsync(id);
            if (pricing == null)
            {
                return NotFound();
            }
            return View(pricing);
        }

        // POST: Pricing/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ChargeAmount,Features,BillingPer,IsActive")] PricePlan pricing)
        {
            if (id != pricing.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pricing);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PricingExists(pricing.Id))
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
            return View(pricing);
        }

        // GET: Pricing/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pricing = await _context.PricePlans
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pricing == null)
            {
                return NotFound();
            }

            return View(pricing);
        }

        // POST: Pricing/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pricing = await _context.PricePlans.FindAsync(id);
            _context.PricePlans.Remove(pricing);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PricingExists(int id)
        {
            return _context.PricePlans.Any(e => e.Id == id);
        }
    }
}
