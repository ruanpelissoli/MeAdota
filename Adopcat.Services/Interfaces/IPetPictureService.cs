using Adopcat.Model;
using System.Threading.Tasks;

namespace Adopcat.Services.Interfaces
{
    public interface IPetPictureService
    {
        Task<PetPicture> Create(byte[] picture, int posterId);
    }
}
