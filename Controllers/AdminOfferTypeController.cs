using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Paris2024.Data;
using Paris2024.Models;

namespace Paris2024.Controllers
{
    public class AdminOfferTypeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminOfferTypeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AdminOfferType
        public async Task<IActionResult> Index()
        {
            return View(await _context.OfferTypes.ToListAsync());
        }

        // GET: AdminOfferType/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var offerType = await _context.OfferTypes
                .FirstOrDefaultAsync(m => m.OfferTypeId == id);
            if (offerType == null)
            {
                return NotFound();
            }

            return View(offerType);
        }

        // GET: AdminOfferType/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AdminOfferType/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OfferTypeId,OfferType_Name,OfferType_AllowedPerson")] OfferType offerType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(offerType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(offerType);
        }

        // GET: AdminOfferType/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var offerType = await _context.OfferTypes.FindAsync(id);
            if (offerType == null)
            {
                return NotFound();
            }
            return View(offerType);
        }

        // POST: AdminOfferType/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OfferTypeId,OfferType_Name,OfferType_AllowedPerson")] OfferType offerType)
        {
            if (id != offerType.OfferTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(offerType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OfferTypeExists(offerType.OfferTypeId))
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
            return View(offerType);
        }

        // GET: AdminOfferType/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var offerType = await _context.OfferTypes
                .FirstOrDefaultAsync(m => m.OfferTypeId == id);
            if (offerType == null)
            {
                return NotFound();
            }

            return View(offerType);
        }

        // POST: AdminOfferType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var offerType = await _context.OfferTypes.FindAsync(id);
            if (offerType != null)
            {
                _context.OfferTypes.Remove(offerType);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OfferTypeExists(int id)
        {
            return _context.OfferTypes.Any(e => e.OfferTypeId == id);
        }
    }
}
