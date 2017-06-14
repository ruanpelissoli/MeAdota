using Adopcat.Model;
using System.Threading.Tasks;

namespace Adopcat.Services.Interfaces
{
    public interface IBlobStorageService
    {
        //PetPicture UploadedImageStorage(ProposalViewModel proposalViewModel);
        Task<string> AddImageToBlobStorageAsync(byte[] file);
    }
}
