using MeAdota.Model;
using System.Threading.Tasks;

namespace MeAdota.Services.Interfaces
{
    public interface IBlobStorageService
    {
        Task<string> AddPetImageToStorageAsync(byte[] file);
        Task<string> AddUserImageToStorageAsync(byte[] file);
        Task DeleteBlobStorageAsync(string url);
    }
}
