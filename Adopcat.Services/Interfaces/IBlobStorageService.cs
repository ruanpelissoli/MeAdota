using Adopcat.Model;
using System.Threading.Tasks;

namespace Adopcat.Services.Interfaces
{
    public interface IBlobStorageService
    {
        Task<string> AddImageToBlobStorageAsync(byte[] file);
        Task DeleteBlobStorageAsync(string url);
    }
}
