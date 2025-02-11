

using System.ComponentModel.DataAnnotations.Schema;

namespace ModelVideotek.Entities;
public class Location
{
    public int Id { get; set; }
    public DateTime DateDebut { get; set; }
    public DateTime DateFin { get; set; }

    [ForeignKey(nameof(Adherent))]
    public int IdAdherent { get; set; }
    public virtual Adherent Adherent { get; set; } = null!;

    [ForeignKey(nameof(Video))]
    public int IdVideo { get; set; }
    public virtual Video Video { get; set; } = null!;
}
