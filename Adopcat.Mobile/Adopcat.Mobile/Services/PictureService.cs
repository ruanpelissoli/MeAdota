using Adopcat.Mobile.Services;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(PictureService))]
namespace Adopcat.Mobile.Services
{
    public class PictureService
    {
        public async Task<MediaFile> PickPhotoAsync()
        {
            await CrossMedia.Current.Initialize();

            var file = await CrossMedia.Current.PickPhotoAsync();
            return file;
        }

        public async Task<MediaFile> TakePhotoAsync()
        {
            await CrossMedia.Current.Initialize();

            var file = await CrossMedia.Current.TakePhotoAsync(
                new StoreCameraMediaOptions
                {
                    SaveToAlbum = true,
                    DefaultCamera = CameraDevice.Rear                    
                });

            return file;
        }
    }
}
