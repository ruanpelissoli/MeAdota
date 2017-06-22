using Adopcat.Mobile.Services;
using Adopcat.Mobile.Util;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Prism.Services;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(PictureService))]
namespace Adopcat.Mobile.Services
{
    public class PictureService
    {
        public async Task<MediaFile> GetPicture(EPictureOptions options)
        {
            try
            {
                switch (options)
                {
                    case EPictureOptions.Pick:
                        return await PickPhotoAsync();
                    case EPictureOptions.Take:
                        return await TakePhotoAsync();
                    default:
                        break;
                }
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
            }
            return null;
        }

        private async Task<MediaFile> PickPhotoAsync()
        {
            await CrossMedia.Current.Initialize();

            var file = await CrossMedia.Current.PickPhotoAsync();
            return file;
        }

        private async Task<MediaFile> TakePhotoAsync()
        {
            await CrossMedia.Current.Initialize();

            var file = await CrossMedia.Current.TakePhotoAsync(
                new StoreCameraMediaOptions
                {
                    AllowCropping = true,
                    SaveToAlbum = true,
                    Directory = "MeAdota",
                    DefaultCamera = CameraDevice.Rear
                });

            return file;
        }
    }
}
