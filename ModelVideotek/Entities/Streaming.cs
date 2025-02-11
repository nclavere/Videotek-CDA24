using System.ComponentModel.DataAnnotations;

namespace ModelVideotek.Entities;
public class Streaming : Video
{
    [StringLength(16)]
    public string Code { get; set; } = null!;
}
