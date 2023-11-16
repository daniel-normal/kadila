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
    public class SalesController : Controller
    {
        private readonly DotnetContext _context;

        public SalesController(DotnetContext context)
        {
            _context = context;
        }

        // GET: Sales
        public async Task<IActionResult> Index()
        {
            var dotnetContext = _context.Sales.Include(s => s.Cliente).Include(s => s.Deuda).Include(s => s.Empleado);
            return View(await dotnetContext.ToListAsync());
        }

        // GET: Sales/Details/5
        public async Task<IActionResult> Details(ulong? id)
        {
            if (id == null || _context.Sales == null)
            {
                return NotFound();
            }

            var sale = await _context.Sales
                .Include(s => s.Cliente)
                .Include(s => s.Deuda)
                .Include(s => s.Empleado)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sale == null)
            {
                return NotFound();
            }

            return View(sale);
        }

        // GET: Sales/Create
        public IActionResult Create()
        {
            ViewData["ClienteId"] = new SelectList(_context.Customers, "Id", "Id");
            ViewData["DeudaId"] = new SelectList(_context.Debts, "Id", "Id");
            ViewData["EmpleadoId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Sales/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FechaVenta,TipoVenta,EmpleadoId,ClienteId,DeudaId,CreatedAt,UpdatedAt")] Sale sale)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sale);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClienteId"] = new SelectList(_context.Customers, "Id", "Id", sale.ClienteId);
            ViewData["DeudaId"] = new SelectList(_context.Debts, "Id", "Id", sale.DeudaId);
            ViewData["EmpleadoId"] = new SelectList(_context.Users, "Id", "Id", sale.EmpleadoId);
            return View(sale);
        }

        // GET: Sales/Edit/5
        public async Task<IActionResult> Edit(ulong? id)
        {
            if (id == null || _context.Sales == null)
            {
                return NotFound();
            }

            var sale = await _context.Sales.FindAsync(id);
            if (sale == null)
            {
                return NotFound();
            }
            ViewData["ClienteId"] = new SelectList(_context.Customers, "Id", "Id", sale.ClienteId);
            ViewData["DeudaId"] = new SelectList(_context.Debts, "Id", "Id", sale.DeudaId);
            ViewData["EmpleadoId"] = new SelectList(_context.Users, "Id", "Id", sale.EmpleadoId);
            return View(sale);
        }

        // POST: Sales/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ulong id, [Bind("Id,FechaVenta,TipoVenta,EmpleadoId,ClienteId,DeudaId,CreatedAt,UpdatedAt")] Sale sale)
        {
            if (id != sale.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sale);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SaleExists(sale.Id))
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
            ViewData["ClienteId"] = new SelectList(_context.Customers, "Id", "Id", sale.ClienteId);
            ViewData["DeudaId"] = new SelectList(_context.Debts, "Id", "Id", sale.DeudaId);
            ViewData["EmpleadoId"] = new SelectList(_context.Users, "Id", "Id", sale.EmpleadoId);
            return View(sale);
        }

        // GET: Sales/Delete/5
        public async Task<IActionResult> Delete(ulong? id)
        {
            if (id == null || _context.Sales == null)
            {
                return NotFound();
            }

            var sale = await _context.Sales
                .Include(s => s.Cliente)
                .Include(s => s.Deuda)
                .Include(s => s.Empleado)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sale == null)
            {
                return NotFound();
            }

            return View(sale);
        }

        // POST: Sales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(ulong id)
        {
            if (_context.Sales == null)
            {
                return Problem("Entity set 'DotnetContext.Sales'  is null.");
            }
            var sale = await _context.Sales.FindAsync(id);
            if (sale != null)
            {
                _context.Sales.Remove(sale);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SaleExists(ulong id)
        {
          return (_context.Sales?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
