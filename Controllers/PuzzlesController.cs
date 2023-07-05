// https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-7.0&tabs=visual-studio-code
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using service.Models;

namespace service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PuzzlesController : ControllerBase
    {
        private readonly PuzzleContext _context;

        public PuzzlesController(PuzzleContext context)
        {
            _context = context;
        }

        // GET: api/Puzzles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Puzzle>>> GetPuzzleItems()
        {
          if (_context.Puzzles == null)
          {
              return NotFound();
          }
            return await _context.Puzzles.ToListAsync();
        }

        // GET: api/Puzzles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Puzzle>> GetPuzzle(string id)
        {
          if (_context.Puzzles == null)
          {
              return NotFound();
          }
            var puzzle = await _context.Puzzles.FindAsync(id);

            if (puzzle == null)
            {
                return NotFound();
            }

            return puzzle;
        }

        // PUT: api/Puzzles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPuzzle(string id, Puzzle puzzle)
        {
            if (id != puzzle.ID)
            {
                return BadRequest();
            }

            _context.Entry(puzzle).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PuzzleExists(id))
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
        public async Task<ActionResult<Puzzle>> PostPuzzle(Puzzle puzzle)
        {
          if (_context.Puzzles == null)
          {
              return Problem("Entity set 'PuzzleContext.PuzzleItems'  is null.");
          }
            _context.Puzzles.Add(puzzle);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (puzzle != null && puzzle.ID != null)
                {
                    if (PuzzleExists(puzzle.ID))
                    {
                        return Conflict();
                    }
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction(nameof(PostPuzzle), new { id = puzzle.ID }, puzzle);
        }

        // DELETE: api/Puzzles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePuzzle(string id)
        {
            if (_context.Puzzles == null)
            {
                return NotFound();
            }
            var puzzle = await _context.Puzzles.FindAsync(id);
            if (puzzle == null)
            {
                return NotFound();
            }

            _context.Puzzles.Remove(puzzle);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PuzzleExists(string id)
        {
            return (_context.Puzzles?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
