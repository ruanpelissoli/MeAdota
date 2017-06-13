using System;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using Adopcat.Mobile.Interfaces;
using System.Collections.ObjectModel;
using System.IO;

namespace Adopcat.Mobile.ViewModels
{
    public class NewPosterPageViewModel : BaseViewModel
    {
        private IPictureService _pictureService;

        private ObservableCollection<byte[]> _petImages;
        public ObservableCollection<byte[]> PetImages
        {
            get { return _petImages; }
            set { SetProperty(ref _petImages, value); }
        }

        private ObservableCollection<string> _petType;
        public ObservableCollection<string> PetType
        {
            get { return _petType; }
            set { SetProperty(ref _petType, value); }
        }

        public DelegateCommand PickPhotoCommand { get; set; }

        public NewPosterPageViewModel(
            INavigationService navigationService, 
            IPageDialogService dialogService,
            IPictureService pictureService) : base(navigationService, dialogService)
        {
            Title = "Novo Anúncio";

            PetImages = new ObservableCollection<byte[]>();
            PetType = new ObservableCollection<string>()
            {
                "Cachorro", "Gato"
            };
            PickPhotoCommand = new DelegateCommand(PickPhotoCommandExecute);
            
            _pictureService = pictureService;
        }

        private async void PickPhotoCommandExecute()
        {
            var file = await _pictureService.TakePhotoFromDevice();

            if(file != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    file.GetStream().CopyTo(memoryStream);
                    file.Dispose();
                    PetImages.Add(memoryStream.ToArray());
                }
            }
        }
    }
}
