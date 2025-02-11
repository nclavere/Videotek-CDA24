using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModelVideotek.Contexts;
using ModelVideotek.Entities;

namespace VideotekAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DVDsController : ControllerBase
    {
        private readonly VideosDbContext _context;

        public DVDsController(VideosDbContext context)
        {
            _context = context;
        }

        // GET: api/DVDs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DVD>>> GetDVDs()
        {
            return await _context.DVDs.ToListAsync();
        }

        // GET: api/DVDs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DVD>> GetDVD(int id)
        {
            var dVD = await _context.DVDs
                .Include(d => d.Realisateurs)
                .Where(d => d.Id == id)
                .FirstOrDefaultAsync();

            if (dVD == null)
            {
                return NotFound();
            }

            return dVD;
        }

        // PUT: api/DVDs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDVD(int id, DVD dVD)
        {
            if (id != dVD.Id)
            {
                return BadRequest();
            }

            _context.Entry(dVD).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DVDExists(id))
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

        // POST: api/DVDs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DVD>> PostDVD(DVD dVD)
        {
            _context.DVDs.Add(dVD);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDVD", new { id = dVD.Id }, dVD);
        }

        // DELETE: api/DVDs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDVD(int id)
        {
            var dVD = await _context.DVDs.FindAsync(id);
            if (dVD == null)
            {
                return NotFound();
            }

            _context.DVDs.Remove(dVD);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DVDExists(int id)
        {
            return _context.DVDs.Any(e => e.Id == id);
        }
    }
}
