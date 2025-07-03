using Microsoft.Extensions.Options;
using Music.Data.Repositories.Interfaces;
using Music.Uploadcare;
using Music.ViewModel;
using Uploadcare;
using Uploadcare.Upload;

namespace Music.Data.Repositories
{
    public class FileRepository : IFileRepository
    {
        private readonly UploadcareClient _uploadcareClient;
        public FileRepository(IOptions<UploadcareKeys> options)
        {
            _uploadcareClient = new UploadcareClient(options.Value.PublicKey, options.Value.PrivateKey);
        }
        private bool IsImage(string contentType)
        {
            return contentType.StartsWith("image/");
        }
        private bool IsMp3(string contentType)
        {
            return contentType.ToLower() == "audio/mpeg";
        }
        public async Task<string> UploadPhotoAsync(IFormFile photo)
        {
            if (!IsImage(photo.ContentType))
            {
                throw new InvalidOperationException("Можно загружать только изображения.");
            }

            using var MemoryStream = new MemoryStream();
            await photo.CopyToAsync(MemoryStream);
            var fileBytes = MemoryStream.ToArray();

            var fileUploader = new FileUploader(_uploadcareClient);
            var uploadPhoto = await fileUploader.Upload(fileBytes, photo.FileName);

            return uploadPhoto.OriginalFileUrl;
        }

        public async Task<string> UploadSongAsync(IFormFile song)
        {
            if (!IsMp3(song.ContentType))
            {
                throw new InvalidOperationException("Можно загружать только MP3 файлы.");
            }

            using var memoryStream = new MemoryStream();
            await song.CopyToAsync(memoryStream);
            var fileBytes = memoryStream.ToArray();

            var fileUploader = new FileUploader(_uploadcareClient);
            var uploadSong = await fileUploader.Upload(fileBytes, song.FileName);

            return uploadSong.OriginalFileUrl;
        }
    }
}
