using Music.Data.Repositories.Interfaces;

namespace Music.Models
{
    public class Album : ISearchable
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required int YearOfIssue { get; set; }
        public required string UrlImg { get; set; }
        public required List<Artist> Artists { get; set; }
        public List<Song> Songs { get; set; }
    }
}
