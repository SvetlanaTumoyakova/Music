namespace Music.ViewModel
{
    public class ArtistViewModel
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public IFormFile File { get; set; }
    }
}
