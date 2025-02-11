using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelVideotek.Entities;
public abstract class Video
{
    public int Id { get; set; }

    [StringLength(240)]
    public string Titre { get; set; } = null!;

    [InverseProperty(nameof(Realisateur.Videos))]
    public List<Realisateur>? Realisateurs { get; set; }
}
