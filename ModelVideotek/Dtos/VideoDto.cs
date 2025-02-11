namespace ModelVideotek.Dtos;

public class VideoDto
{
    public int Id { get; set; }
    public string Titre { get; set; } = null!;
    public string TypeVideo { get; set; } = null!;
    public List<string>? Realisateurs { get; set; } 

}
