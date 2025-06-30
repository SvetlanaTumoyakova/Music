namespace Music.ViewModel
{
    public class AlbumViewModel
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required int YearOfIssue { get; set; }
        public required string ArtistName { get; set; }
        public IFormFile File { get; set; }
    }
}
