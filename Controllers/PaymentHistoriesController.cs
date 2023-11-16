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
    public class PaymentHistoriesController : Controller
    {
        private readonly DotnetContext _context;

        public PaymentHistoriesController(DotnetContext context)
        {
            _context = context;
        }

        // GET: PaymentHistories
        public async Task<IActionResult> Index()
        {
            var dotnetContext = _context.PaymentHistories.Include(p => p.Deuda);
            return View(await dotnetContext.ToListAsync());
        }

        // GET: PaymentHistories/Details/5
        public async Task<IActionResult> Details(ulong? id)
        {
            if (id == null || _context.PaymentHistories == null)
            {
                return NotFound();
            }

            var paymentHistory = await _context.PaymentHistories
                .Include(p => p.Deuda)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paymentHistory == null)
            {
                return NotFound();
            }

            return View(paymentHistory);
        }

        // GET: PaymentHistories/Create
        public IActionResult Create()
        {
            ViewData["DeudaId"] = new SelectList(_context.Debts, "Id", "Id");
            return View();
        }

        // POST: PaymentHistories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FechaPago,Monto,DeudaId,CreatedAt,UpdatedAt")] PaymentHistory paymentHistory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(paymentHistory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DeudaId"] = new SelectList(_context.Debts, "Id", "Id", paymentHistory.DeudaId);
            return View(paymentHistory);
        }

        // GET: PaymentHistories/Edit/5
        public async Task<IActionResult> Edit(ulong? id)
        {
            if (id == null || _context.PaymentHistories == null)
            {
                return NotFound();
            }

            var paymentHistory = await _context.PaymentHistories.FindAsync(id);
            if (paymentHistory == null)
            {
                return NotFound();
            }
            ViewData["DeudaId"] = new SelectList(_context.Debts, "Id", "Id", paymentHistory.DeudaId);
            return View(paymentHistory);
        }

        // POST: PaymentHistories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ulong id, [Bind("Id,FechaPago,Monto,DeudaId,CreatedAt,UpdatedAt")] PaymentHistory paymentHistory)
        {
            if (id != paymentHistory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(paymentHistory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaymentHistoryExists(paymentHistory.Id))
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
            ViewData["DeudaId"] = new SelectList(_context.Debts, "Id", "Id", paymentHistory.DeudaId);
            return View(paymentHistory);
        }

        // GET: PaymentHistories/Delete/5
        public async Task<IActionResult> Delete(ulong? id)
        {
            if (id == null || _context.PaymentHistories == null)
            {
                return NotFound();
            }

            var paymentHistory = await _context.PaymentHistories
                .Include(p => p.Deuda)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paymentHistory == null)
            {
                return NotFound();
            }

            return View(paymentHistory);
        }

        // POST: PaymentHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(ulong id)
        {
            if (_context.PaymentHistories == null)
            {
                return Problem("Entity set 'DotnetContext.PaymentHistories'  is null.");
            }
            var paymentHistory = await _context.PaymentHistories.FindAsync(id);
            if (paymentHistory != null)
            {
                _context.PaymentHistories.Remove(paymentHistory);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaymentHistoryExists(ulong id)
        {
          return (_context.PaymentHistories?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
