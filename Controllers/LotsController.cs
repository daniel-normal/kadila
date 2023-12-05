using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using kadila.Data;
using kadila.Models;
using System.Diagnostics;

namespace kadila.Controllers
{
    [Authorize]
    public class LotsController : Controller
    {
        private readonly DotnetContext _context;

        public LotsController(DotnetContext context)
        {
            _context = context;
        }

        // GET: Lots
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["CurrentFilter"] = searchString;

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            var lots = _context.Lots.Include(l => l.Producto).AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
            {
                lots = lots.Where(l => l.Nombre.Contains(searchString));
            }

            int pageSize = 10;
            return View(await PaginatedList<Lot>.CreateAsync(lots.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Lots/Details/5
        public async Task<IActionResult> Details(ulong? id)
        {
            if (id == null || _context.Lots == null)
            {
                return NotFound();
            }

            var lot = await _context.Lots
                .Include(l => l.Producto)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lot == null)
            {
                return NotFound();
            }

            return View(lot);
        }

        // GET: Lots/Create
        public IActionResult Create()
        {
            var products = _context.Products
                .Select(p => new SelectListItem
                {
                    Value = p.Id.ToString(),
                    Text = $"{p.Nombre} {p.Precio}"
                })
                .ToList();
            products.Insert(0, new SelectListItem { Value = "", Text = "SELECCIONAR PRODUCTO" });
            ViewData["Lots"] = products;
            return View();
        }

        // POST: Lots/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Cantidad,PrecioActual,ProductoId,CreatedAt,UpdatedAt")] Lot lot)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lot);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductoId"] = new SelectList(_context.Products, "Id", "Id", lot.ProductoId);
            return View(lot);
        }

        // GET: Lots/Edit/5
        public async Task<IActionResult> Edit(ulong? id)
        {
            if (id == null || _context.Lots == null)
            {
                return NotFound();
            }

            var lot = await _context.Lots.FindAsync(id);
            if (lot == null)
            {
                return NotFound();
            }
            ViewData["ProductoId"] = new SelectList(_context.Products, "Id", "Id", lot.ProductoId);
            return View(lot);
        }

        // POST: Lots/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ulong id, [Bind("Id,Nombre,Cantidad,PrecioActual,ProductoId,CreatedAt,UpdatedAt")] Lot lot)
        {
            if (id != lot.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lot);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LotExists(lot.Id))
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
            ViewData["ProductoId"] = new SelectList(_context.Products, "Id", "Id", lot.ProductoId);
            return View(lot);
        }

        // GET: Lots/Delete/5
        public async Task<IActionResult> Delete(ulong? id)
        {
            if (id == null || _context.Lots == null)
            {
                return NotFound();
            }

            var lot = await _context.Lots
                .Include(l => l.Producto)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lot == null)
            {
                return NotFound();
            }

            return View(lot);
        }

        // POST: Lots/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(ulong id)
        {
            if (_context.Lots == null)
            {
                return Problem("Entity set 'DotnetContext.Lots'  is null.");
            }
            var lot = await _context.Lots.FindAsync(id);
            if (lot != null)
            {
                _context.Lots.Remove(lot);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LotExists(ulong id)
        {
          return (_context.Lots?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
