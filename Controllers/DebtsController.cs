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

namespace kadila.Controllers
{
    [Authorize]
    public class DebtsController : Controller
    {
        private readonly DotnetContext _context;

        public DebtsController(DotnetContext context)
        {
            _context = context;
        }

        // GET: Debts
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

            var debts = _context.Debts.Include(d => d.Cliente).AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
            {
                debts = debts.Where(d => d.Estado.Contains(searchString) ||
                                         d.Cliente.Nombre.Contains(searchString) ||
                                         d.Cliente.Apellido.Contains(searchString));
            }

            int pageSize = 10;
            return View(await PaginatedList<Debt>.CreateAsync(debts.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Debts/Details/5
        public async Task<IActionResult> Details(ulong? id)
        {
            if (id == null || _context.Debts == null)
            {
                return NotFound();
            }

            var debt = await _context.Debts
                .Include(d => d.Cliente)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (debt == null)
            {
                return NotFound();
            }

            return View(debt);
        }

        // GET: Debts/Create
        public IActionResult Create()
        {
            var customers = _context.Customers
                .Select(r => new SelectListItem
                {
                    Value = r.Id.ToString(),
                    Text = string.IsNullOrEmpty(r.Apellido) ? r.Nombre : $"{r.Nombre} {r.Apellido}"
                })
                .ToList();
            customers.Insert(0, new SelectListItem { Value = "", Text = "SELECCIONAR CLIENTE" });
            ViewData["Debts"] = customers;
            return View();
        }


        // POST: Debts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FechaLimite,FechaCreacion,Monto,Estado,ClienteId,CreatedAt,UpdatedAt")] Debt debt)
        {
            if (ModelState.IsValid)
            {
                _context.Add(debt);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "SE REGISTRÓ LA DEUDA.";
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClienteId"] = new SelectList(_context.Customers, "Id", "Id", debt.ClienteId);
            return View(debt);
        }

        // GET: Debts/Edit/5
        public async Task<IActionResult> Edit(ulong? id)
        {
            if (id == null || _context.Debts == null)
            {
                return NotFound();
            }

            var debt = await _context.Debts.FindAsync(id);
            if (debt == null)
            {
                return NotFound();
            }

            var customers = _context.Customers
                .Select(r => new SelectListItem
                {
                    Value = r.Id.ToString(),
                    Text = string.IsNullOrEmpty(r.Apellido) ? r.Nombre : $"{r.Nombre} {r.Apellido}"
                })
                .ToList();

            foreach (var customer in customers)
            {
                if (customer.Value == debt.ClienteId.ToString())
                {
                    customer.Selected = true;
                    break;
                }
            }

            ViewData["ClienteId"] = customers;

            return View(debt);
        }


        // POST: Debts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ulong id, [Bind("Id,FechaLimite,FechaCreacion,Monto,Estado,ClienteId,CreatedAt,UpdatedAt")] Debt debt)
        {
            if (id != debt.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(debt);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DebtExists(debt.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["WarningMessage"] = "SE ACTUALIZARON LOS DATOS DE LA DEUDA.";
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClienteId"] = new SelectList(_context.Customers, "Id", "Id", debt.ClienteId);
            return View(debt);
        }

        // GET: Debts/Delete/5
        public async Task<IActionResult> Delete(ulong? id)
        {
            if (id == null || _context.Debts == null)
            {
                return NotFound();
            }

            var debt = await _context.Debts
                .Include(d => d.Cliente)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (debt == null)
            {
                return NotFound();
            }

            return View(debt);
        }

        // POST: Debts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(ulong id)
        {
            if (_context.Debts == null)
            {
                return Problem("Entity set 'DotnetContext.Debts'  is null.");
            }
            var debt = await _context.Debts.FindAsync(id);
            if (debt != null)
            {
                _context.Debts.Remove(debt);
            }
            
            await _context.SaveChangesAsync();
            TempData["DangerMessage"] = "SE ELIMINÓ LA DEUDA.";
            return RedirectToAction(nameof(Index));
        }
        
        private bool DebtExists(ulong id)
        {
          return (_context.Debts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}