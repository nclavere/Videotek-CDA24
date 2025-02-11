using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModelVideotek.Contexts;
using ModelVideotek.Dtos;
using ModelVideotek.Entities;
using ModelVideotek.Extensions;

/// <summary>
/// Ce controleur permet de créer, modifier et supprimer des DVD
/// Donc on a supprimé les méthodes GET
/// Et on retourne la route GetVideo du controleur Videos après la création
/// </summary>
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

            //on gère l'état des realisateurs pour les créer ou les modifier
            dVD.Realisateurs?.ForEach(r => _context.Entry(r).State = ( r.Id == 0 ? EntityState.Added : EntityState.Unchanged));

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

            // Après la création, on renvoie le VideoDto de la méthode GetVideo du controller Videos
            return CreatedAtAction("GetVideo", "Videos", 
                new { id = dVD.Id }, dVD.ToDto());
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
