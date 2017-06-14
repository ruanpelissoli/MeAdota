using System;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using Adopcat.Mobile.Interfaces;
using System.Collections.ObjectModel;
using System.IO;
using Adopcat.Mobile.Models;
using Adopcat.Mobile.Helpers;
using System.Linq;
using System.Diagnostics;

namespace Adopcat.Mobile.ViewModels
{
    public class NewPosterPageViewModel : BaseViewModel
    {
        private IPictureService _pictureService;

        private ObservableCollection<byte[]> _petImages;
        public ObservableCollection<byte[]> PetImages
        {
            get { return _petImages; }
            set
            {
                SetProperty(ref _petImages, value);
                CreatePosterCommand.RaiseCanExecuteChanged();
            }
        }

        private ObservableCollection<string> _petTypeList;
        public ObservableCollection<string> PetTypeList
        {
            get { return _petTypeList; }
            set { SetProperty(ref _petTypeList, value); }
        }

        private string _petType;
        public string PetType
        {
            get { return _petType; }
            set
            {
                SetProperty(ref _petType, value);
                CreatePosterCommand.RaiseCanExecuteChanged();
            }
        }

        private bool _isCastrated;
        public bool IsCastrated
        {
            get { return _isCastrated; }
            set { SetProperty(ref _isCastrated, value); }
        }

        private bool _isDewormed;
        public bool IsDewormed
        {
            get { return _isDewormed; }
            set { SetProperty(ref _isDewormed, value); }
        }

        private bool _deliverToAdopter;
        public bool DeliverToAdopter
        {
            get { return _deliverToAdopter; }
            set { SetProperty(ref _deliverToAdopter, value); }
        }
        
        public DelegateCommand PickPhotoCommand { get; set; }
        public DelegateCommand CreatePosterCommand { get; set; }
        public DelegateCommand<string> PetTypeSelectCommand { get; set; }

        public NewPosterPageViewModel(
            INavigationService navigationService,
            IPageDialogService dialogService,
            IPictureService pictureService) : base(navigationService, dialogService)
        {
            Title = "Novo Anúncio";

            PetImages = new ObservableCollection<byte[]>();
            PetTypeList = new ObservableCollection<string>()
            {
                "Cachorro", "Gato"
            };
            IsCastrated = false;
            IsDewormed = false;
            DeliverToAdopter = false;

            PickPhotoCommand = new DelegateCommand(PickPhotoCommandExecute);
            CreatePosterCommand = new DelegateCommand(CreatePosterCommandExecute, CreatePosterCommandCanExecute);
            PetTypeSelectCommand = new DelegateCommand<string>(PetTypeSelectCommandExecute);

            _pictureService = pictureService;
        }

        private void PetTypeSelectCommandExecute(string petType)
        {
            PetType = petType;
        }

        private async void CreatePosterCommandExecute()
        {
            try
            {
                var posterInput = new PosterInput
                {
                    UserId = int.Parse(Settings.UserId),
                    PetPictures = PetImages.ToList(),
                    PetType = PetType == "Cachorro" ? 1 : 2,
                    Castrated = IsCastrated,
                    Dewormed = IsDewormed,
                    DeliverToAdopter = DeliverToAdopter,
                };

                await App.ApiService.CreatePoster(posterInput, Settings.AuthToken);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
            }
        }

        private bool CreatePosterCommandCanExecute()
        {
            return PetImages.Any() &&
                   !string.IsNullOrEmpty(PetType);
        }

        private async void PickPhotoCommandExecute()
        {
            var file = await _pictureService.TakePhotoFromDevice();

            if (file != null)
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
