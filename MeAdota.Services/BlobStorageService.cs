using MeAdota.Data.Interfaces;
using MeAdota.Model;
using MeAdota.Services.Interfaces;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MeAdota.Services
{
    public class BlobStorageService : BaseService, IBlobStorageService
    {
        private IApplicationParameterRepository _applicationParameterRepository;

        private string _azureStorageUrl;
        private string _azureStoragePublicContainer;
        private string _azureStorageConnectionString;
        private string _petPicturesBlobName;
        private string _userPicturesBlobName;

        public BlobStorageService(ILoggingService log, IApplicationParameterRepository applicationParameterRepository) : base(log)
        {
            _applicationParameterRepository = applicationParameterRepository;
            var parameters = _applicationParameterRepository.LoadParameters();
            parameters.TryGetValue("AzureStorageURL", out _azureStorageUrl);
            parameters.TryGetValue("AzureStoragePublicContainer", out _azureStoragePublicContainer);
            parameters.TryGetValue("PetPicturesBlobName", out _petPicturesBlobName);
            parameters.TryGetValue("UserPicturesBlobName", out _userPicturesBlobName);
            parameters.TryGetValue("AzureStorageConnectionString", out _azureStorageConnectionString);
        }

        public async Task<string> AddPetImageToStorageAsync(byte[] file)
        {
            //  get the container reference
            var container = GetImagesBlobContainer();

            // using the container reference, get a block blob reference and set its type
            var guidName = Guid.NewGuid().ToString();
            CloudBlockBlob blockBlob = container.GetBlockBlobReference($"{_petPicturesBlobName}/{guidName}");
            blockBlob.Properties.ContentType = "image/jpg";

            // finally, upload the image into blob storage using the block blob reference
            await blockBlob.UploadFromByteArrayAsync(file, 0, file.Length);

            return $"{_azureStorageUrl}/{_azureStoragePublicContainer}/{_petPicturesBlobName}/{guidName}";
        }

        public async Task<string> AddUserImageToStorageAsync(byte[] file)
        {
            //  get the container reference
            var container = GetImagesBlobContainer();

            // using the container reference, get a block blob reference and set its type
            var guidName = Guid.NewGuid().ToString();
            CloudBlockBlob blockBlob = container.GetBlockBlobReference($"{_userPicturesBlobName}/{guidName}");
            blockBlob.Properties.ContentType = "image/jpg";

            // finally, upload the image into blob storage using the block blob reference
            await blockBlob.UploadFromByteArrayAsync(file, 0, file.Length);

            return $"{_azureStorageUrl}/{_azureStoragePublicContainer}/{_userPicturesBlobName}/{guidName}";
        }

        public async Task DeleteBlobStorageAsync(string url)
        {
            try
            {
                //  get the container reference
                var container = GetImagesBlobContainer();

                var blobName = url.Split('/').Last();
                // using the container reference, get a block blob reference and set its type
                CloudBlockBlob blockBlob = container.GetBlockBlobReference($"{_petPicturesBlobName}/{blobName}");

                // finally, upload the image into blob storage using the block blob reference
                await blockBlob.DeleteAsync();
            }
            catch (Exception)
            {
            }
            
        }

        private CloudBlobContainer GetImagesBlobContainer()
        {
            // use the connection string to get the storage account
            var storageAccount = CloudStorageAccount.Parse(_azureStorageConnectionString);

            // using the storage account, create the blob client
            var blobClient = storageAccount.CreateCloudBlobClient();

            // finally, using the blob client, get a reference to our container
            var container = blobClient.GetContainerReference(_azureStoragePublicContainer);

            // if we had not created the container in the portal, this would automatically create it for us at run time
            container.CreateIfNotExists();

            // by default, blobs are private and would require your access key to download.
            //   You can allow public access to the blobs by making the container public.   
            container.SetPermissions(
                new BlobContainerPermissions
                {
                    PublicAccess = BlobContainerPublicAccessType.Blob
                });


            return container;
        }
    }
}
