using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using kadila.Data;
using kadila.Models;

namespace kadila.Controllers
{
    public class SaleDetailsController : Controller
    {
        private readonly DotnetContext _context;

        public SaleDetailsController(DotnetContext context)
        {
            _context = context;
        }

        // GET: SaleDetails
        public async Task<IActionResult> Index()
        {
            var dotnetContext = _context.SaleDetails.Include(s => s.Producto).Include(s => s.Venta);
            return View(await dotnetContext.ToListAsync());
        }

        // GET: SaleDetails/Details/5
        public async Task<IActionResult> Details(ulong? id)
        {
            if (id == null || _context.SaleDetails == null)
            {
                return NotFound();
            }

            var saleDetail = await _context.SaleDetails
                .Include(s => s.Producto)
                .Include(s => s.Venta)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (saleDetail == null)
            {
                return NotFound();
            }

            return View(saleDetail);
        }

        // GET: SaleDetails/Create
        public IActionResult Create()
        {
            ViewData["ProductoId"] = new SelectList(_context.Products, "Id", "Id");
            ViewData["VentaId"] = new SelectList(_context.Sales, "Id", "Id");
            return View();
        }

        // POST: SaleDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Cantidad,Precio,VentaId,ProductoId,CreatedAt,UpdatedAt")] SaleDetail saleDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(saleDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductoId"] = new SelectList(_context.Products, "Id", "Id", saleDetail.ProductoId);
            ViewData["VentaId"] = new SelectList(_context.Sales, "Id", "Id", saleDetail.VentaId);
            return View(saleDetail);
        }

        // GET: SaleDetails/Edit/5
        public async Task<IActionResult> Edit(ulong? id)
        {
            if (id == null || _context.SaleDetails == null)
            {
                return NotFound();
            }

            var saleDetail = await _context.SaleDetails.FindAsync(id);
            if (saleDetail == null)
            {
                return NotFound();
            }
            ViewData["ProductoId"] = new SelectList(_context.Products, "Id", "Id", saleDetail.ProductoId);
            ViewData["VentaId"] = new SelectList(_context.Sales, "Id", "Id", saleDetail.VentaId);
            return View(saleDetail);
        }

        // POST: SaleDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ulong id, [Bind("Id,Cantidad,Precio,VentaId,ProductoId,CreatedAt,UpdatedAt")] SaleDetail saleDetail)
        {
            if (id != saleDetail.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(saleDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SaleDetailExists(saleDetail.Id))
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
            ViewData["ProductoId"] = new SelectList(_context.Products, "Id", "Id", saleDetail.ProductoId);
            ViewData["VentaId"] = new SelectList(_context.Sales, "Id", "Id", saleDetail.VentaId);
            return View(saleDetail);
        }

        // GET: SaleDetails/Delete/5
        public async Task<IActionResult> Delete(ulong? id)
        {
            if (id == null || _context.SaleDetails == null)
            {
                return NotFound();
            }

            var saleDetail = await _context.SaleDetails
                .Include(s => s.Producto)
                .Include(s => s.Venta)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (saleDetail == null)
            {
                return NotFound();
            }

            return View(saleDetail);
        }

        // POST: SaleDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(ulong id)
        {
            if (_context.SaleDetails == null)
            {
                return Problem("Entity set 'DotnetContext.SaleDetails'  is null.");
            }
            var saleDetail = await _context.SaleDetails.FindAsync(id);
            if (saleDetail != null)
            {
                _context.SaleDetails.Remove(saleDetail);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SaleDetailExists(ulong id)
        {
          return (_context.SaleDetails?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
