using ModelVideotek.Dtos;
using ModelVideotek.Entities;

namespace ModelVideotek.Extensions;
public static class VideoExtension
{
    public static VideoDto ToDto(this Video video)
    {
        return new VideoDto
        {
            Id = video.Id,
            Titre = video.Titre,
            TypeVideo = video.GetType().Name,
            Realisateurs = video.Realisateurs != null ?
                video.Realisateurs.Select(r => string.Join(" ", r.Prenom, r.Nom)).ToList() : null
        };
    }
}
