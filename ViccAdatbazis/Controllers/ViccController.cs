using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ViccAdatbazis.Models;

namespace ViccAdatbazis.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ViccController : ControllerBase
    {
        private readonly ViccDbContext _context;

        public ViccController(ViccDbContext context)
        {
            _context = context;
        }

        // GET: api/jokes?page=1
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vicc>>> GetJokes([FromQuery] int page = 1)
        {
            int pageSize = 10;
            var jokes = await _context.Viccek
                                      .Where(j => !j.Aktiv)
                                      .Skip((page - 1) * pageSize)
                                      .Take(pageSize)
                                      .ToListAsync();

            return Ok(jokes);
        }

        // GET: api/jokes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Vicc>> GetJoke(int id)
        {
            var joke = await _context.Viccek.FindAsync(id);

            if (joke == null)
            {
                return NotFound();
            }

            return Ok(joke);
        }

        // POST: api/jokes
        [HttpPost]
        public async Task<ActionResult<Vicc>> AddJoke(Vicc newJoke)
        {
            _context.Viccek.Add(newJoke);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetJoke), new { id = newJoke.Id }, newJoke);
        }

        // PUT: api/jokes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> EditJoke(int id, Vicc updatedJoke)
        {
            if (id != updatedJoke.Id)
            {
                return BadRequest();
            }

            var joke = await _context.Viccek.FindAsync(id);
            if (joke == null)
            {
                return NotFound();
            }

            joke.Tartalom = updatedJoke.Tartalom;
            joke.Feltolto = updatedJoke.Feltolto;
            joke.Tetszik = updatedJoke.Tetszik;
            joke.NemTetszik = updatedJoke.NemTetszik;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // PATCH: api/jokes/5/like
        [HttpPatch("{id}/like")]
        public async Task<IActionResult> LikeJoke(int id)
        {
            var joke = await _context.Viccek.FindAsync(id);
            if (joke == null)
            {
                return NotFound();
            }

            joke.Tetszik++;
            await _context.SaveChangesAsync();

            return Ok(joke);
        }

        // PATCH: api/jokes/5/dislike
        [HttpPatch("{id}/dislike")]
        public async Task<IActionResult> DislikeJoke(int id)
        {
            var joke = await _context.Viccek.FindAsync(id);
            if (joke == null)
            {
                return NotFound();
            }

            joke.NemTetszik++;
            await _context.SaveChangesAsync();

            return Ok(joke);
        }

        // DELETE: api/jokes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> ArchiveJoke(int id)
        {
            var joke = await _context.Viccek.FindAsync(id);
            if (joke == null)
            {
                return NotFound();
            }

            joke.Aktiv = true;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/jokes/archive/5
        [HttpDelete("archive/{id}")]
        public async Task<IActionResult> DeleteArchivedJoke(int id)
        {
            var joke = await _context.Viccek.FindAsync(id);
            if (joke == null || !joke.Aktiv)
            {
                return NotFound();
            }

            _context.Viccek.Remove(joke);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
