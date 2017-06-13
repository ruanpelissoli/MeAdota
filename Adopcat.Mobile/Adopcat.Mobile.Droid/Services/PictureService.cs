using System;
using System.Threading.Tasks;
using Plugin.Media.Abstractions;
using Plugin.Media;
using Adopcat.Mobile.Droid.Services;
using Adopcat.Mobile.Interfaces;

[assembly: Xamarin.Forms.Dependency(typeof(PictureService))]
namespace Adopcat.Mobile.Droid.Services
{
    public class PictureService : IPictureService
    {
        public async Task<MediaFile> TakePhotoFromDevice()
        {
            await CrossMedia.Current.Initialize();

            var file = await CrossMedia.Current.PickPhotoAsync();
            return file;
        }
    }
}