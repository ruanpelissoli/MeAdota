using Plugin.Media.Abstractions;
using System.Threading.Tasks;

namespace Adopcat.Mobile.Interfaces
{
    public interface IPictureService
    {
        Task<MediaFile> TakePhotoFromDevice();
    }
}
