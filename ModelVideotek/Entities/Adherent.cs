
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace ModelVideotek.Entities;

[Index(nameof(Email), IsUnique = true)]
public class Adherent : Personne
{
    [StringLength(10)]
    public string NoAdherent { get; set; } = null!;

    [StringLength(10)]
    public string? Telephone { get; set; }

    [StringLength(160)]
    public string Email { get; set; } = null!;

    [InverseProperty(nameof(Location.Adherent))]
    public List<Location>? Locations { get; set; }



}
