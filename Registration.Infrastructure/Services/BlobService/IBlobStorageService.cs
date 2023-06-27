using Microsoft.AspNetCore.Http;

namespace Registration.Infrastructure.Services.BlobService
{
    public interface IBlobStorageService
    {
        Task<string> GetBlobUrl(string imageName);
        Task RemoveBlob(string imageName);
        Task<string> UploadBlob(IFormFile formFile, string imageName);
    }
}