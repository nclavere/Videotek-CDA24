using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModelVideotek.Contexts;
using ModelVideotek.Dtos;
using ModelVideotek.Entities;

/// <summary>
/// Ce controleur permet de créer, modifier et supprimer des BluRays
/// Donc on a supprimé les méthodes GET
/// Et on retourne la route GetVideo du controleur Videos après la création
/// </summary>
namespace VideotekAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BluRaysController : ControllerBase
    {
        private readonly VideosDbContext _context;

        public BluRaysController(VideosDbContext context)
        {
            _context = context;
        }

        // PUT: api/BluRays/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBluRay(int id, BluRay bluRay)
        {
            if (id != bluRay.Id)
            {
                return BadRequest();
            }

            _context.Entry(bluRay).State = EntityState.Modified;

            //on gère l'état des realisateurs pour les créer ou les modifier
            bluRay.Realisateurs?.ForEach(r => _context.Entry(r).State = (r.Id == 0 ? EntityState.Added : EntityState.Unchanged));


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BluRayExists(id))
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

        // POST: api/BluRays
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BluRay>> PostBluRay(BluRay bluRay)
        {
            _context.BluRays.Add(bluRay);
            await _context.SaveChangesAsync();

            // Après la création, on renvoie le VideoDto de la méthode GetVideo du controller Videos
            return CreatedAtAction("GetVideo", "Videos", 
                new { id = bluRay.Id },
                new VideoDto { Id = bluRay.Id, Titre = bluRay.Titre });
        }

        // DELETE: api/BluRays/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBluRay(int id)
        {
            var bluRay = await _context.BluRays.FindAsync(id);
            if (bluRay == null)
            {
                return NotFound();
            }

            _context.BluRays.Remove(bluRay);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BluRayExists(int id)
        {
            return _context.BluRays.Any(e => e.Id == id);
        }
    }
}
