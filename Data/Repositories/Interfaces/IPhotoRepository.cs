namespace Music.Data.Repositories.Interfaces
{
    public interface IPhotoRepository
    {
        Task<string> UploadPhotoAsync(IFormFile photo);
    }
}
