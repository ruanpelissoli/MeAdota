﻿using System;
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
using Adopcat.Mobile.Services;
using Adopcat.Mobile.Views;
using Plugin.Media.Abstractions;

namespace Adopcat.Mobile.ViewModels
{
    public class NewPosterPageViewModel : BaseViewModel
    {
        private ObservableCollection<PetPictureItem> _petImages;
        public ObservableCollection<PetPictureItem> PetImages
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

        private string _petName;
        public string PetName
        {
            get { return _petName; }
            set
            {
                SetProperty(ref _petName, value);
                CreatePosterCommand.RaiseCanExecuteChanged();
            }
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
        public DelegateCommand<string> DeletePetPictureCommand { get; set; }

        public NewPosterPageViewModel(
            INavigationService navigationService,
            IPageDialogService dialogService) : base(navigationService, dialogService)
        {
            Title = "Novo Anúncio";

            PickPhotoCommand = new DelegateCommand(PickPhotoCommandExecute);
            CreatePosterCommand = new DelegateCommand(CreatePosterCommandExecute, CreatePosterCommandCanExecute);
            PetTypeSelectCommand = new DelegateCommand<string>(PetTypeSelectCommandExecute);
            DeletePetPictureCommand = new DelegateCommand<string>(DeletePetPictureCommandExecute);

            PetImages = new ObservableCollection<PetPictureItem>();
            PetTypeList = new ObservableCollection<string>()
            {
                "Cachorro", "Gato"
            };
            IsCastrated = false;
            IsDewormed = false;
            DeliverToAdopter = false;
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
                    PetName = PetName,
                    PetPictures = PetImages.Select(s => s.Image).ToList(),
                    PetType = PetType == "Cachorro" ? 1 : 2,
                    Castrated = IsCastrated,
                    Dewormed = IsDewormed,
                    DeliverToAdopter = DeliverToAdopter,
                    Country = "Brasil",
                    State = "RS",
                    City = "Porto Alegre"
                };

                await App.ApiService.CreatePoster(posterInput, "bearer " + Settings.AuthToken);
                await _dialogService.DisplayAlertAsync("Sucesso!", "Anúncio criado com sucesso.", "Ok");
                await _navigationService.NavigateAsync($"app:///{nameof(MenuPage)}/NavigationPage/{nameof(MyPostersPage)}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
            }
        }

        private bool CreatePosterCommandCanExecute()
        {
            return PetImages.Any() &&
                   !string.IsNullOrEmpty(PetName) &&
                   !string.IsNullOrEmpty(PetType);
        }

        private async void PickPhotoCommandExecute()
        {
            var action = await _dialogService.DisplayActionSheetAsync("Foto", "Cancel", null, "Tirar foto", "Álbum");

            MediaFile file;
            var pictureService = Xamarin.Forms.DependencyService.Get<PictureService>();

            if (action.ToLower().Equals("tirar foto"))
                file = await pictureService.TakePhotoAsync();
            else
                file = await pictureService.PickPhotoAsync();

            if (file != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    file.GetStream().CopyTo(memoryStream);
                    file.Dispose();
                    PetImages.Add(new PetPictureItem
                    {
                        Id = Guid.NewGuid().ToString(),
                        Image = memoryStream.ToArray()
                    });
                    CreatePosterCommand.RaiseCanExecuteChanged();
                }
            }
        }

        private void DeletePetPictureCommandExecute(string imageId)
        {
            PetImages.Remove(PetImages.Single(w => w.Id == imageId));
            CreatePosterCommand.RaiseCanExecuteChanged();
        }
    }
}