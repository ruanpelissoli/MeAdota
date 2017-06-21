using Adopcat.Mobile.Helpers;
using Adopcat.Mobile.Models;
using Adopcat.Mobile.Services;
using Adopcat.Mobile.Util;
using Adopcat.Mobile.Views;
using Plugin.Media.Abstractions;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Adopcat.Mobile.ViewModels
{
    public class EditMyPosterPageViewModel : BaseViewModel
    {
        private PosterOutput _poster;
        public PosterOutput Poster
        {
            get { return _poster; }
            set { SetProperty(ref _poster, value); }
        }

        private ObservableCollection<PetPictureItem> _petImages;
        public ObservableCollection<PetPictureItem> PetImages
        {
            get { return _petImages; }
            set
            {
                SetProperty(ref _petImages, value);
                EditPosterCommand.RaiseCanExecuteChanged();
            }
        }

        private ObservableCollection<string> _petTypeList;
        public ObservableCollection<string> PetTypeList
        {
            get { return _petTypeList; }
            set { SetProperty(ref _petTypeList, value); }
        }

        private string _state;
        public string State
        {
            get { return _state; }
            set { SetProperty(ref _state, value); }
        }

        private ObservableCollection<string> _stateList;
        public ObservableCollection<string> StatesList
        {
            get { return _stateList; }
            set { SetProperty(ref _stateList, value); }
        }

        private string _petType;
        public string PetType
        {
            get { return _petType; }
            set { SetProperty(ref _petType, value); }
        }

        public DelegateCommand PickPhotoCommand { get; set; }
        public DelegateCommand EditPosterCommand { get; set; }
        public DelegateCommand<string> PetTypeSelectCommand { get; set; }
        public DelegateCommand<string> DeletePetPictureCommand { get; set; }
        public DelegateCommand<string> StateSelectCommand { get; set; }

        public EditMyPosterPageViewModel(INavigationService navigationService, IPageDialogService dialogService) : base(navigationService, dialogService)
        {
            Title = "Editar anúncio";

            PickPhotoCommand = new DelegateCommand(PickPhotoCommandExecute);
            EditPosterCommand = new DelegateCommand(EditPosterCommandExecute, EditPosterCommandCanExecute);
            PetTypeSelectCommand = new DelegateCommand<string>(PetTypeSelectCommandExecute);
            DeletePetPictureCommand = new DelegateCommand<string>(DeletePetPictureCommandExecute);
            StateSelectCommand = new DelegateCommand<string>(StateSelectCommandExecute);

            PetImages = new ObservableCollection<PetPictureItem>();
            PetTypeList = new ObservableCollection<string>()
            {
                "Cachorro", "Gato"
            };
            StatesList = new ObservableCollection<string>(StateList.Get());
        }

        private void StateSelectCommandExecute(string state)
        {
            Poster.State = state;
        }

        private void DeletePetPictureCommandExecute(string imageId)
        {
            PetImages.Remove(PetImages.Single(w => w.Id == imageId));
            EditPosterCommand.RaiseCanExecuteChanged();
        }

        private void PetTypeSelectCommandExecute(string petType)
        {
            Poster.PetType = petType == "Cachorro" ? 1 : 2;
        }

        private bool EditPosterCommandCanExecute()
        {
            return PetImages.Any() &&
                   !string.IsNullOrEmpty(Poster.PetName) &&
                   Poster.PetType > 0;
        }

        private async void EditPosterCommandExecute()
        {
            try
            {
                var posterInput = new PosterInput
                {
                    Id = Poster.Id,
                    PetName = Poster.PetName,
                    PetPictures = PetImages.Select(s => s.Image).ToList(),
                    PetType = PetType == "Cachorro" ? 1 : 2,
                    Castrated = Poster.Castrated,
                    Dewormed = Poster.Dewormed,
                    IsAdopted = Poster.IsAdopted,
                    Country = "Brasil",
                    State = Poster.State,
                    City = Poster.City
                };

                await App.ApiService.UpdatePoster(posterInput, "bearer " + Settings.AuthToken);
                await _dialogService.DisplayAlertAsync("Sucesso!", "Anúncio atualizado com sucesso.", "Ok");
                await _navigationService.NavigateAsync($"app:///{nameof(MenuPage)}/NavigationPage/{nameof(MyPostersPage)}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
            }
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
                    EditPosterCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public async override void OnNavigatedTo(NavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            try
            {
                if (parameters.Any(a => a.Key.Equals("posterId")))
                {
                    Poster = await App.ApiService.GetPoster(parameters.GetValue<int>("posterId"), "bearer " + Settings.AuthToken);
                    PetType = Poster.PetType == 1 ? "Cachorro" : "Gato";

                    foreach (var item in Poster.PetPictures)
                    {
                        byte[] file;
                        try
                        {
                            file = await App.ApiService.Download(item.Url);
                        } catch { continue; }

                        PetImages.Add(new PetPictureItem
                        {
                            Id = Guid.NewGuid().ToString(),
                            Image = file
                        });
                    }
                    EditPosterCommand.RaiseCanExecuteChanged();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
                throw;
            }           
        }
    }
}