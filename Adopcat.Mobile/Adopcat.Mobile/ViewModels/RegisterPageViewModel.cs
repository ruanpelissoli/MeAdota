using System;
using Prism.Commands;
using Prism.Navigation;
using Adopcat.Mobile.Models;
using System.Diagnostics;
using Prism.Services;
using Adopcat.Mobile.Views;

namespace Adopcat.Mobile.ViewModels
{
    public class RegisterPageViewModel : BaseViewModel
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                SetProperty(ref _name, value);
                RegisterCommand.RaiseCanExecuteChanged();
            }
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set
            {
                SetProperty(ref _email, value);
                RegisterCommand.RaiseCanExecuteChanged();
            }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                SetProperty(ref _password, value);
                RegisterCommand.RaiseCanExecuteChanged();
            }
        }

        private string _passwordConfirm;
        public string PasswordConfirm
        {
            get { return _passwordConfirm; }
            set
            {
                SetProperty(ref _passwordConfirm, value);
                RegisterCommand.RaiseCanExecuteChanged();
            }
        }

        private string _phone;
        public string Phone
        {
            get { return _phone; }
            set
            {
                SetProperty(ref _phone, value);
                RegisterCommand.RaiseCanExecuteChanged();
            }
        }

        public DelegateCommand RegisterCommand { get; set; }

        public RegisterPageViewModel(INavigationService navigationService, IPageDialogService dialogService) : base(navigationService, dialogService)
        {
            Title = "Registre-se";

            RegisterCommand = new DelegateCommand(RegisterCommandExecute, RegisterCommandCanExecute);
        }

        private bool RegisterCommandCanExecute()
        {
            return !string.IsNullOrEmpty(Name) &&
                   !string.IsNullOrEmpty(Email) &&
                   !string.IsNullOrEmpty(Password) &&
                   !string.IsNullOrEmpty(PasswordConfirm) &&
                   Password == PasswordConfirm &&
                   !string.IsNullOrEmpty(Phone);
        }

        private async void RegisterCommandExecute()
        {
            try
            {
                ShowLoading = true;

                var user = new User
                {
                    Email = Email,
                    Name = Name,
                    Password = Password,
                    Phone = Phone
                };

                await App.ApiService.CreateUser(user);

                await _dialogService.DisplayAlertAsync("Sucesso", "Usuário criado com sucesso.", "Ok");

                ShowLoading = false;
                await _navigationService.NavigateAsync(nameof(LoginDataPage));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
            }
        }
    }
}
