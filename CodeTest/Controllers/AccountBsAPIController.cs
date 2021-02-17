using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CodeTest.Models;

namespace CodeTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountBsAPIController : ControllerBase
    {
        private readonly DatabaseBContext _context;

        public AccountBsAPIController(DatabaseBContext context)
        {
            _context = context;
        }

        // GET: api/AccountBsAPI
        [HttpGet]
        public IEnumerable<AccountB> GetAccountB()
        {
            return _context.AccountB;
        }

        // GET: api/AccountBsAPI/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccountB([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var accountB = await _context.AccountB.FindAsync(id);

            if (accountB == null)
            {
                return NotFound();
            }

            return Ok(accountB);
        }

        // PUT: api/AccountBsAPI/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccountB([FromRoute] int id, [FromBody] AccountB accountB)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != accountB.Id)
            {
                return BadRequest();
            }

            _context.Entry(accountB).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
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

            return NoContent();
        }

        // POST: api/AccountBsAPI
        [HttpPost]
        public async Task<IActionResult> PostAccountB([FromBody] AccountB accountB)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.AccountB.Add(accountB);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAccountB", new { id = accountB.Id }, accountB);
        }

        // DELETE: api/AccountBsAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccountB([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var accountB = await _context.AccountB.FindAsync(id);
            if (accountB == null)
            {
                return NotFound();
            }

            _context.AccountB.Remove(accountB);
            await _context.SaveChangesAsync();

            return Ok(accountB);
        }

        private bool AccountBExists(int id)
        {
            return _context.AccountB.Any(e => e.Id == id);
        }
    }
}