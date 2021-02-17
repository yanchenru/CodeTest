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
    public class QueryController : Controller
    {
        private readonly DatabaseAContext _contextA;

        private readonly DatabaseBContext _contextB;

        public QueryController(DatabaseAContext contextA, DatabaseBContext contextB)
        {
            _contextA = contextA;
            _contextB = contextB;
        }
                
        // GET: QueryAccount
        public IActionResult QueryAccount()
        {
            return View();
        }

        // Post: QueryResults
        public async Task<IActionResult> QueryResults(String AccountNo)
        {
            QueryResults qr = new QueryResults();
            qr.AccountAs = await _contextA.AccountA.Where(j => j.AccountNo.Contains(AccountNo)).ToListAsync();
            qr.AccountBs = await _contextB.AccountB.Where(j => j.AccountNo.Contains(AccountNo)).ToListAsync();

            return View(qr);
        }

        // GET: Query/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accountB = await _contextB.AccountB
                .FirstOrDefaultAsync(m => m.Id == id);
            if (accountB == null)
            {
                return NotFound();
            }

            return View(accountB);
        }

        // GET: Query/Create
        public IActionResult Create()
        {
            return View();
        }

        // GET: Query/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accountB = await _contextA.AccountA.FindAsync(id);
            if (accountB == null)
            {
                return NotFound();
            }
            return View(accountB);
        }

        // POST: Query/Edit/5
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
                    _contextA.Update(accountB);
                    await _contextA.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountAExists(accountB.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(QueryResults));
            }
            return View(accountB);
        }       

        private bool AccountAExists(int id)
        {
            return _contextB.AccountB.Any(e => e.Id == id);
        }
    }
}
