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
    public class AccountBsController : Controller
    {
        private readonly DatabaseBContext _context;

        public AccountBsController(DatabaseBContext context)
        {
            _context = context;
        }

        // GET: AccountBs
        public async Task<IActionResult> Index()
        {
            return View(await _context.AccountB.ToListAsync());
        }

        // GET: AccountBs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accountB = await _context.AccountB
                .FirstOrDefaultAsync(m => m.Id == id);
            if (accountB == null)
            {
                return NotFound();
            }

            return View(accountB);
        }

        // GET: AccountBs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AccountBs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AccountNo,WithdrawalDate")] AccountB accountB)
        {
            if (ModelState.IsValid)
            {
                _context.Add(accountB);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(accountB);
        }

        // GET: AccountBs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accountB = await _context.AccountB.FindAsync(id);
            if (accountB == null)
            {
                return NotFound();
            }
            return View(accountB);
        }

        // POST: AccountBs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AccountNo,WithdrawalDate")] AccountB accountB)
        {
            if (id != accountB.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(accountB);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountBExists(accountB.Id))
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
            return View(accountB);
        }

        // GET: AccountBs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accountB = await _context.AccountB
                .FirstOrDefaultAsync(m => m.Id == id);
            if (accountB == null)
            {
                return NotFound();
            }

            return View(accountB);
        }

        // POST: AccountBs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var accountB = await _context.AccountB.FindAsync(id);
            _context.AccountB.Remove(accountB);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccountBExists(int id)
        {
            return _context.AccountB.Any(e => e.Id == id);
        }
    }
}
