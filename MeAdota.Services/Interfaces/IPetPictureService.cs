using MeAdota.Model;
using System.Threading.Tasks;

namespace MeAdota.Services.Interfaces
{
    public interface IPetPictureService
    {
        Task<PetPicture> Create(byte[] picture, int posterId);
        Task Delete(PetPicture petPicture);
        Task DeleteMany(int posterId);
    }
}
