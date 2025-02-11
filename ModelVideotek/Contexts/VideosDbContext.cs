using Microsoft.EntityFrameworkCore;
using ModelVideotek.Entities;

namespace ModelVideotek.Contexts;
public class VideosDbContext : DbContext
{
    public virtual DbSet<Adherent> Adherents { get; set; } = null!;
    public virtual DbSet<BluRay> BluRays { get; set; } = null!;
    public virtual DbSet<DVD> DVDs { get; set; } = null!;
    public virtual DbSet<Location> Locations { get; set; } = null!;
    public virtual DbSet<Personne> Personnes { get; set; } = null!;
    public virtual DbSet<Realisateur> Realisateurs { get; set; } = null!;
    public virtual DbSet<Streaming> Streamings { get; set; } = null!;
    public virtual DbSet<Video> Videos { get; set; } = null!;

    public VideosDbContext(DbContextOptions<VideosDbContext> options)
        : base(options)
    {
    }
}
