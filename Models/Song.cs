using Music.Data.Repositories.Interfaces;

namespace Music.Models
{
    public class Song : ISearchable
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string UrlSong { get; set; }
        public required List<Album> Albums { get; set; }
        public required List<Artist> Artists { get; set; }
    }
}
