using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ViccAdatbazis.Models;

namespace ViccAdatbazis.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ViccController : ControllerBase
    {
        //Adatbázis kapcsolat
        private readonly ViccDbContext _context;
        public ViccController(ViccDbContext context)
        {
            _context = context;
        }
        //Viccek lekérdezése
        [HttpGet]
        public async Task<ActionResult<List<Vicc>>> GetViccek()
        {
            return await _context.Viccek.Where(x => x.Aktiv == true).ToListAsync();
        }

        //Egy vicc lekérdezése
        [HttpGet("{id}")]
        public async Task<ActionResult<Vicc>> GetVicc(int id)
        {
            var vicc = await _context.Viccek.FindAsync(id);

            if (vicc == null)
            {
                return NotFound();
            }
            return vicc;
        }

        //Vicc feltöltése
        [HttpPost]
        public async Task<ActionResult<Vicc>> PostVicc(Vicc vicc)
        {
            _context.Viccek.Add(vicc);
            await _context.SaveChangesAsync();

            //return Ok();

            //A válasz maga a vicc
            return CreatedAtAction("GetVicc", new { id = vicc.Id }, vicc);
        }

        //Vicc módosítása
        [HttpPut("{id}")]
        public async Task<ActionResult> PutVicc(int id, Vicc vicc)
        {
            if (id != vicc.Id)
            {
                return BadRequest();
            }
            _context.Entry(vicc).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteVicc(int id)
        {
            var vicc = await _context.Viccek.FindAsync(id);

            if (vicc == null) { return NotFound(); }

            if (vicc.Aktiv == true) { vicc.Aktiv = false; }
            else { _context.Viccek.Remove(vicc); }

            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
