using Azure.Data.Tables;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Sas;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registration.Infrastructure.Services.BlobService
{
    public class BlobStorageService : IBlobStorageService
    {
        private const string containerName = "attendeeImages";
        private readonly IConfiguration _configuration;

        public BlobStorageService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> UploadBlob(IFormFile formFile, string imageName)
        {
            var blobName = $"{imageName}{Path.GetExtension(formFile.FileName)}";
            var container = await GetBlobContainerClient();
            using var memoryStream = new MemoryStream();
            formFile.CopyTo(memoryStream);
            memoryStream.Position = 0;
            var client = await container.UploadBlobAsync(blobName, memoryStream);
            return blobName;
        }

        public async Task<string> GetBlobUrl(string imageName)
        {
            var container = await GetBlobContainerClient();
            var blob = container.GetBlobClient(imageName);
            BlobSasBuilder blobSasBuilder = new BlobSasBuilder
            {
                BlobContainerName = blob.BlobContainerName,
                BlobName = blob.Name,
                ExpiresOn = DateTime.UtcNow.AddMinutes(5),
                Protocol = SasProtocol.Https,
                Resource = "b"
            };
            blobSasBuilder.SetPermissions(BlobAccountSasPermissions.Read);
            return blob.GenerateSasUri(blobSasBuilder).ToString();
        }

        public async Task RemoveBlob(string imageName)
        {
            var container = await GetBlobContainerClient();
            var blob = container.GetBlobClient(imageName);
            await blob.DeleteIfExistsAsync(DeleteSnapshotsOption.IncludeSnapshots);
        }

        private async Task<BlobContainerClient> GetBlobContainerClient()
        {
            try
            {
                var container = new BlobContainerClient(_configuration["StorageConnectionString"], containerName);

                await container.CreateIfNotExistsAsync();

                return container;

            }
            catch (Exception ex)
            {

                throw;
            }

        }
    }
}
