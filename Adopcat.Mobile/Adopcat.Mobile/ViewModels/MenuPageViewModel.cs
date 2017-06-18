using Adopcat.Mobile.Helpers;
using Adopcat.Mobile.Models;
using Adopcat.Mobile.Views;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Adopcat.Mobile.ViewModels
{
    public class MenuPageViewModel : BaseViewModel
    {
        private User _user;
        public User User
        {
            get { return _user; }
            set { SetProperty(ref _user, value); }
        }

        private ObservableCollection<MenuItem> _menuItems;
        public ObservableCollection<MenuItem> MenuItems
        {
            get { return _menuItems; }
            set { SetProperty(ref _menuItems, value); }
        }

        public DelegateCommand<string> MenuClickedCommand { get; set; }

        public MenuPageViewModel(INavigationService navigationService, IPageDialogService dialogService) : base(navigationService, dialogService)
        {
            var menuItemsList = new List<MenuItem>
            {
                new MenuItem { Text = "Anúncios", Icon = "icon_menu_posters.png", GoTo = nameof(PostersPage) },
                new MenuItem { Text = "Anunciar", Icon = "icon_menu_newposter.png", GoTo = nameof(NewPosterPage) },
                new MenuItem { Text = "Meus Anúncios", Icon = "icon_menu_myposters.png", GoTo = nameof(MyPostersPage) },
                new MenuItem { Text = "Configurações", Icon = "icon_menu_config.png", GoTo = nameof(ConfigurationPage) },
            };

            MenuItems = new ObservableCollection<MenuItem>(menuItemsList);

            MenuClickedCommand = new DelegateCommand<string>(MenuClickedCommandExecute);
        }

        private async void MenuClickedCommandExecute(string page)
        {
            await _navigationService.NavigateAsync($"NavigationPage/{page}");
        }

        public async override void OnNavigatedTo(NavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            try
            {
                if (User == null)
                    User = await App.ApiService.GetUser(int.Parse(Settings.UserId));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
            }
        }
    }
}
