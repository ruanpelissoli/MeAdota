using Adopcat.Data.Interfaces;
using Adopcat.Model;
using Adopcat.Services.Interfaces;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Threading.Tasks;

namespace Adopcat.Services
{
    public class BlobStorageService : BaseService, IBlobStorageService
    {
        private IApplicationParameterRepository _applicationParameterRepository;

        private string _imageRootPath;
        private string _containerName;
        private string _blobStorageConnectionString;
        private string _blobName;

        public BlobStorageService(ILoggingService log, IApplicationParameterRepository applicationParameterRepository) : base(log)
        {
            _applicationParameterRepository = applicationParameterRepository;
            var parameters = _applicationParameterRepository.LoadParameters();
            parameters.TryGetValue("ImageRootPath", out _imageRootPath);
            parameters.TryGetValue("ErrorImagesContainer", out _containerName);
            parameters.TryGetValue("ErrorImagesBlob", out _blobName);
            parameters.TryGetValue("BlobStorageConnectionString", out _blobStorageConnectionString);
        }

        //public PetPicture UploadedImageStorage(ProposalViewModel proposalViewModel)
        //{
        //    var screenError = Convert.FromBase64String(proposalViewModel.ScreenErrorBase64);

        //    var imageName = $"{proposalViewModel.ProposalId}_{DateTime.Now.ToString("yyyy-MM-dd_hhmmss")}.jpg";

        //    return new UploadedImageModel
        //    {
        //        ContentType = "image/jpeg",
        //        Data = screenError,
        //        Name = imageName,
        //        Url = $"{_imageRootPath}/{_containerName}/{_blobName}/{imageName}"
        //    };
        //}

        public async Task AddImageToBlobStorageAsync(byte[] file)
        {
            //  get the container reference
            var container = GetImagesBlobContainer();

            // using the container reference, get a block blob reference and set its type
            CloudBlockBlob blockBlob = container.GetBlockBlobReference($"{_blobName}/{Guid.NewGuid().ToString()}");
            blockBlob.Properties.ContentType = "jpg";

            // finally, upload the image into blob storage using the block blob reference
            await blockBlob.UploadFromByteArrayAsync(file, 0, file.Length);
        }

        private CloudBlobContainer GetImagesBlobContainer()
        {
            // use the connection string to get the storage account
            var storageAccount = CloudStorageAccount.Parse(_blobStorageConnectionString);

            // using the storage account, create the blob client
            var blobClient = storageAccount.CreateCloudBlobClient();

            // finally, using the blob client, get a reference to our container
            var container = blobClient.GetContainerReference(_containerName);

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
