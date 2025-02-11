using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModelVideotek.Contexts;
using ModelVideotek.Entities;

/// <summary>
/// Ce controleur permet de créer, modifier et supprimer des Streaming
/// Donc on a supprimé les méthodes GET
/// Et on retourne la route GetVideo du controleur Videos après la création
/// </summary>
namespace VideotekAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StreamingsController : ControllerBase
    {
        private readonly VideosDbContext _context;

        public StreamingsController(VideosDbContext context)
        {
            _context = context;
        }


        // PUT: api/Streamings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStreaming(int id, Streaming streaming)
        {
            if (id != streaming.Id)
            {
                return BadRequest();
            }

            _context.Entry(streaming).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StreamingExists(id))
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

        // POST: api/Streamings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Streaming>> PostStreaming(Streaming streaming)
        {
            _context.Streamings.Add(streaming);
            await _context.SaveChangesAsync();

            // Après la création, on renvoie le VideoDto de la méthode GetVideo du controller Videos
            return CreatedAtAction("GetVideo", "Videos", new { id = streaming.Id }, streaming);
        }

        // DELETE: api/Streamings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStreaming(int id)
        {
            var streaming = await _context.Streamings.FindAsync(id);
            if (streaming == null)
            {
                return NotFound();
            }

            _context.Streamings.Remove(streaming);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StreamingExists(int id)
        {
            return _context.Streamings.Any(e => e.Id == id);
        }
    }
}
