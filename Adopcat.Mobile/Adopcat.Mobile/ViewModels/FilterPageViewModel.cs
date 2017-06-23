using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System.Collections.ObjectModel;
using System;
using Adopcat.Mobile.Models;
using Adopcat.Mobile.Views;

namespace Adopcat.Mobile.ViewModels
{
    public class FilterPageViewModel : BaseViewModel
    {
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
            }
        }

        private string _petType;
        public string PetType
        {
            get { return _petType; }
            set
            {
                SetProperty(ref _petType, value);
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

        private string _city;
        public string City
        {
            get { return _city; }
            set { SetProperty(ref _city, value); }
        }

        public DelegateCommand<string> PetTypeSelectCommand { get; set; }
        public DelegateCommand FilterCommand { get; set; }
        public DelegateCommand ClearFilterCommand { get; set; }

        public FilterPageViewModel(INavigationService navigationService, IPageDialogService dialogService) : base(navigationService, dialogService)
        {
            Title = "Filtro";

            PetTypeSelectCommand = new DelegateCommand<string>(PetTypeSelectCommandExecute);
            FilterCommand = new DelegateCommand(FilterCommandExecute);
            ClearFilterCommand = new DelegateCommand(ClearFilterCommandExecute);

            PetTypeList = new ObservableCollection<string>()
            {
                "Cachorro", "Gato"
            };
            IsCastrated = false;
            IsDewormed = false;
            DeliverToAdopter = false;
        }

        private async void ClearFilterCommandExecute()
        {
            await _navigationService.NavigateAsync($"app:///{nameof(MenuPage)}/NavigationPage/{nameof(LoadingPostersPage)}", null);
        }

        private async void FilterCommandExecute()
        {
            var filter = new Filter();
            if (!string.IsNullOrEmpty(PetType))
                filter.PetType = PetType == "Cachorro" ? 1 : 2;
            filter.Castrated = IsCastrated;
            filter.Dewormed = IsDewormed;
            filter.DeliverToAdopter = DeliverToAdopter;
            filter.City = City;

            var parameters = new NavigationParameters
            {
                { "filter", filter }
            };

            await _navigationService.NavigateAsync($"app:///{nameof(MenuPage)}/NavigationPage/{nameof(LoadingPostersPage)}", parameters);
        }

        private void PetTypeSelectCommandExecute(string petType)
        {
            PetType = petType;
        }

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
        }

        public override void OnNavigatedFrom(NavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);
        }
    }
}
