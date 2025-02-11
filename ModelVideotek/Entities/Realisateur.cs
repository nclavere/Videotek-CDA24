using System.ComponentModel.DataAnnotations.Schema;

namespace ModelVideotek.Entities;
public class Realisateur : Personne
{
    [InverseProperty(nameof(Video.Realisateurs))]
    public List<Video>? Videos { get; set; }
}
