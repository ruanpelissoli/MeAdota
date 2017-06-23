using Adopcat.Model;
using System.Threading.Tasks;

namespace Adopcat.Services.Interfaces
{
    public interface IBlobStorageService
    {
        Task<string> AddPetImageToStorageAsync(byte[] file);
        Task<string> AddUserImageToStorageAsync(byte[] file);
        Task DeleteBlobStorageAsync(string url);
    }
}
