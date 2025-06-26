using Microsoft.Extensions.Options;
using Music.Data.Repositories.Interfaces;
using Music.Uploadcare;
using Music.ViewModel;
using Uploadcare;
using Uploadcare.Upload;

namespace Music.Data.Repositories
{
    public class PhotoRepository : IPhotoRepository
    {
        private readonly UploadcareClient _uploadcareClient;
        public PhotoRepository(IOptions<UploadcareKeys> options)
        {
            _uploadcareClient = new UploadcareClient(options.Value.PublicKey, options.Value.PrivateKey);
        }
        public async Task<string> UploadPhotoAsync(IFormFile photo)
        {
            using var MemoryStream = new MemoryStream();
            await photo.CopyToAsync(MemoryStream);
            var fileBytes = MemoryStream.ToArray();

            var fileUploader = new FileUploader(_uploadcareClient);
            var uploadPhoto = await fileUploader.Upload(fileBytes, photo.FileName);

            return uploadPhoto.OriginalFileUrl;
        }
    }
}
