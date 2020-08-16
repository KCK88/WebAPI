using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AllItemsController : ControllerBase
    {
        private readonly AllContext _context;

        public AllItemsController(AllContext context)
        {
            _context = context;
        }

        // GET: api/AllItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AllItem>>> GetAllItems()
        {
            return await _context.AllItems.ToListAsync();
        }

        // GET: api/AllItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AllItem>> GetAllItem(long id)
        {
            var allItem = await _context.AllItems.FindAsync(id);

            if (allItem == null)
            {
                return NotFound();
            }

            return allItem;
        }

        // PUT: api/AllItems/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAllItem(long id, AllItem allItem)
        {
            if (id != allItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(allItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AllItemExists(id))
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

        // POST: api/AllItems
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<AllItem>> PostAllItem(AllItem allItem)
        {
            _context.AllItems.Add(allItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAllItem", new { id = allItem.Id }, allItem);
        }

        // DELETE: api/AllItems/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AllItem>> DeleteAllItem(long id)
        {
            var allItem = await _context.AllItems.FindAsync(id);
            if (allItem == null)
            {
                return NotFound();
            }

            _context.AllItems.Remove(allItem);
            await _context.SaveChangesAsync();

            return allItem;
        }

        private bool AllItemExists(long id)
        {
            return _context.AllItems.Any(e => e.Id == id);
        }
    }
}
