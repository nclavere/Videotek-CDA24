using System.ComponentModel.DataAnnotations;

namespace ModelVideotek.Entities;
public abstract class Personne
{
    public int Id { get; set; }

    [StringLength(80)]
    public string Nom { get; set; } = null!;

    [StringLength(80)]
    public string Prenom { get; set; } = null!;
}
