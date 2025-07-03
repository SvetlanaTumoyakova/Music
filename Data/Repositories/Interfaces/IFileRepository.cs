namespace Music.Data.Repositories.Interfaces
{
    public interface IFileRepository
    {
        Task<string> UploadPhotoAsync(IFormFile photo);
        Task<string> UploadSongAsync(IFormFile song);
    }
}
