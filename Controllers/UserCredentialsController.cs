using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using service.Models;

namespace service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserCredentialsController : ControllerBase
    {
        private readonly PuzzleContext _context;

        public UserCredentialsController(PuzzleContext context)
        {
            _context = context;
        }

        // GET: api/Puzzles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserCredentials>>> GetUserCredentialsItems()
        {
          if (_context.UserCredentials == null)
          {
              return NotFound();
          }
            return await _context.UserCredentials.ToListAsync();
        }

        // GET: api/Puzzles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserCredentials>> GetUserCredentials(string id)
        {
          if (_context.UserCredentials == null)
          {
              return NotFound();
          }
            var userCredentials = await _context.UserCredentials.FindAsync(id);

            if (userCredentials == null)
            {
                return NotFound();
            }

            return userCredentials;
        }

        // PUT: api/Puzzles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserCredentials(int id, UserCredentials userCredentials)
        {
            if (id != userCredentials.ID)
            {
                return BadRequest();
            }

            _context.Entry(userCredentials).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserCredentialsExists(id))
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

        // POST: api/Puzzles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserCredentials>> PostUserCredentials(UserCredentials userCredentials)
        {
          if (_context.UserCredentials == null)
          {
              return Problem("Entity set 'PuzzleContext.UserCredentials'  is null.");
          }
            _context.UserCredentials.Add(userCredentials);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (userCredentials != null && userCredentials.ID != 0)
                {
                    if (UserCredentialsExists(userCredentials.ID))
                    {
                        return Conflict();
                    }
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction(nameof(PostUserCredentials), new { id = userCredentials.ID }, userCredentials);
        }

        // DELETE: api/Puzzles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserCredentials(int id)
        {
            if (_context.UserCredentials == null)
            {
                return NotFound();
            }
            var userCredentials = await _context.UserCredentials.FindAsync(id);
            if (userCredentials == null)
            {
                return NotFound();
            }

            _context.UserCredentials.Remove(userCredentials);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserCredentialsExists(int id)
        {
            return (_context.UserCredentials?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}