using ModelVideotek.Contexts;
using ModelVideotek.Entities;

using var context =new VideosDbContext();

//context.DVDs.Add(new DVD
//{
//    Titre = "Star Wars",
//});
//context.DVDs.Add(new DVD
//{
//    Titre = "Matrix",
//});
//context.DVDs.Add(new DVD
//{
//    Titre = "Dune",
//});
//context.DVDs.Add(new DVD
//{
//    Titre = "Le silence des agneaux",
//});
//context.SaveChanges();

var video = context.DVDs
    .Where(video => video.Titre == "Star Wars")
    .First();
video.Realisateurs = new List<Realisateur>
{
    new Realisateur
    {
        Nom = "Lucas",
        Prenom = "George",
    }
};
context.SaveChanges();



var lstVideos = context.Videos
    .Where( video => video.Titre.StartsWith("Star"))
    .OrderByDescending(video => video.Titre)
    .ToList();

foreach (var item in lstVideos)
{
    Console.WriteLine($"{item.Titre} : type = {item.GetType()}" );
}

Console.ReadKey();
