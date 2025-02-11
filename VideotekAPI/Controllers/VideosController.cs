using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModelVideotek.Contexts;
using ModelVideotek.Dtos;
using ModelVideotek.Entities;

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
