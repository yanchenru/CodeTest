using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CodeTest.Models;

namespace CodeTest.Controllers
{
    public class AccountAsController : Controller
    {
        private readonly DatabaseAContext _context;

        public AccountAsController(DatabaseAContext context)
        {
            _context = context;
        }

        // GET: AccountAs
        public async Task<IActionResult> Index()
        {
            return View(await _context.AccountA.ToListAsync());
        }

        // GET: AccountAs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accountA = await _context.AccountA
                .FirstOrDefaultAsync(m => m.Id == id);
            if (accountA == null)
            {
                return NotFound();
            }

            return View(accountA);
        }

        // GET: AccountAs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AccountAs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AccountNo,WithdrawalDate")] AccountA accountA)
        {
            if (ModelState.IsValid)
            {
                _context.Add(accountA);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(accountA);
        }

        // GET: AccountAs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accountA = await _context.AccountA.FindAsync(id);
            if (accountA == null)
            {
                return NotFound();
            }
            return View(accountA);
        }

        // POST: AccountAs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AccountNo,WithdrawalDate")] AccountA accountA)
        {
            if (id != accountA.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(accountA);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountAExists(accountA.Id))
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
            return View(accountA);
        }

        // GET: AccountAs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accountA = await _context.AccountA
                .FirstOrDefaultAsync(m => m.Id == id);
            if (accountA == null)
            {
                return NotFound();
            }

            return View(accountA);
        }

        // POST: AccountAs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var accountA = await _context.AccountA.FindAsync(id);
            _context.AccountA.Remove(accountA);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccountAExists(int id)
        {
            return _context.AccountA.Any(e => e.Id == id);
        }
    }
}
