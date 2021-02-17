using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CodeTest.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;

namespace CodeTest.Controllers
{
    public class CompareController : Controller
    {
        private readonly DatabaseAContext _contextA;

        private readonly DatabaseBContext _contextB;

        public CompareController(DatabaseAContext contextA, DatabaseBContext contextB)
        {
            _contextA = contextA;
            _contextB = contextB;
        }

        // GET: Compare
        public async Task<IActionResult> Index()
        {
            QueryResults qr = new QueryResults();
            qr.AccountAs = await _contextA.AccountA.ToListAsync();
            qr.AccountBs = await _contextB.AccountB.ToListAsync();

            return View(qr);
        }

        // GET: Compare/Edit/5
        public async Task<IActionResult> Edit(int id, string accountNo, string withdrawalDate)
        {
            try
            {
                AccountB receivedAccountB = new AccountB();
                receivedAccountB.Id = id;
                receivedAccountB.AccountNo = accountNo;
                receivedAccountB.WithdrawalDate = withdrawalDate;

                using (var httpClient = new HttpClient())
                {
                    var content = JsonConvert.SerializeObject(receivedAccountB);

                    var request = new StringContent(content, Encoding.UTF8, "application/json");

                    await httpClient.PutAsync("https://localhost:44345/api/AccountBsAPI/"+id, request);
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountBExists(id))
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

        private bool AccountBExists(int id)
        {
            return _contextB.AccountB.Any(e => e.Id == id);
        }
    }
}
