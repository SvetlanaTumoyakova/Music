using Music.Data.Repositories.Interfaces;

namespace Music.Models
{
    public class Artist : ISearchable
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string UrlImg { get; set; }
        public List<Album> Albums { get; set; }
        public List<Song> Songs { get; set; }
    }
}
