using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModelVideotek.Contexts;
using ModelVideotek.Dtos;

/// <summary>
/// Ce controleur permet de voir le catalogue de videos quel que soit leur type
/// Donc on ne garde que les méthodes GET
/// Et on renvoie un VideoDto qui contient le type de la video
/// </summary>
namespace VideotekAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideosController : ControllerBase
    {
        private readonly VideosDbContext _context;

        public VideosController(VideosDbContext context)
        {
            _context = context;
        }

        // GET: api/Videos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VideoDto>>> GetVideos()
        {
            return await _context.Videos
                .Include(d => d.Realisateurs)
                .Select(d => new VideoDto
                {
                    Id = d.Id,
                    Titre = d.Titre,
                    TypeVideo = d.GetType().Name.ToString(),
                    Realisateurs = d.Realisateurs != null ?
                        d.Realisateurs.Select(r => string.Join(" ", r.Prenom, r.Nom)).ToList() : null
                })
                .ToListAsync();
        }

        // GET: api/Videos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VideoDto>> GetVideo(int id)
        {
            var video = await _context.Videos
                .Include(d => d.Realisateurs)
                .Where(d => d.Id == id)
                .Select(d => new VideoDto
                {
                    Id = d.Id,
                    Titre = d.Titre,
                    TypeVideo = d.GetType().Name.ToString(),
                    Realisateurs = d.Realisateurs != null ?
                        d.Realisateurs.Select(r => string.Join(" ", r.Prenom, r.Nom)).ToList() : null
                })
                .FirstOrDefaultAsync();

            if (video == null)
            {
                return NotFound();
            }

            return video;
        }

    }
}
