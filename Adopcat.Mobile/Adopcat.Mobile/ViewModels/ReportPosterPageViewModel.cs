using System;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System.Diagnostics;
using Adopcat.Mobile.Models;
using System.Collections.Generic;
using Adopcat.Mobile.Helpers;

namespace Adopcat.Mobile.ViewModels
{
    public class ReportPosterPageViewModel : BaseViewModel
    {
        private bool _isOffensive;
        public bool IsOffensive
        {
            get { return _isOffensive; }
            set { SetProperty(ref _isOffensive, value); }
        }

        private string _isOffensiveText;
        public string IsOffensiveText
        {
            get { return _isOffensiveText; }
            set
            {
                SetProperty(ref _isOffensiveText, value);
                SendReportCommand.RaiseCanExecuteChanged();
            }
        }

        private bool _isNotADogOrCat;
        public bool IsNotADogOrCat
        {
            get { return _isNotADogOrCat; }
            set { SetProperty(ref _isNotADogOrCat, value); }
        }

        private string _isNotADogOrCatText;
        public string IsNotADogOrCatText
        {
            get { return _isNotADogOrCatText; }
            set
            {
                SetProperty(ref _isNotADogOrCatText, value);
                SendReportCommand.RaiseCanExecuteChanged();
            }
        }

        private bool _problemWithUser;
        public bool ProblemWithUser
        {
            get { return _problemWithUser; }
            set { SetProperty(ref _problemWithUser, value); }
        }

        private string _problemWithUserText;
        public string ProblemWithUserText
        {
            get { return _problemWithUserText; }
            set
            {
                SetProperty(ref _problemWithUserText, value);
                SendReportCommand.RaiseCanExecuteChanged();
            }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set
            {
                SetProperty(ref _description, value);
                SendReportCommand.RaiseCanExecuteChanged();
            }
        }

        public DelegateCommand SendReportCommand { get; set; }
        public DelegateCommand CloseCommand { get; set; }

        public ReportPosterPageViewModel(INavigationService navigationService, IPageDialogService dialogService)
            : base(navigationService, dialogService)
        {
            Title = "Denunciar";

            SendReportCommand = new DelegateCommand(SendReportCommandExecute, SendReportCommandCanExecute);
            CloseCommand = new DelegateCommand(CloseCommandExecute);
        }

        private int PosterId { get; set; }

        private async void SendReportCommandExecute()
        {
            ShowLoading = true;
            try
            {
                var reasons = new List<string>();

                if (!string.IsNullOrEmpty(IsOffensiveText))
                    reasons.Add(IsOffensiveText);

                if (!string.IsNullOrEmpty(IsNotADogOrCatText))
                    reasons.Add(IsNotADogOrCatText);

                if (!string.IsNullOrEmpty(ProblemWithUserText))
                    reasons.Add(ProblemWithUserText);

                var report = new Reports
                {
                    PosterId = PosterId,
                    Motive = string.Join(";", reasons.ToArray()),
                    Description = Description
                };

                await App.ApiService.CreateReport(report, "bearer " + Settings.AuthToken);

                await _dialogService.DisplayAlertAsync("Sucesso!", "Denúncia enviada com sucesso.", "Ok");

                IsOffensive = false;
                IsOffensiveText = string.Empty;

                IsNotADogOrCat = false;
                IsNotADogOrCatText = string.Empty;

                ProblemWithUser = false;
                ProblemWithUserText = string.Empty;

                Description = string.Empty;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
            }finally
            {
                ShowLoading = false;
            }
        }

        private bool SendReportCommandCanExecute()
        {
            return (!string.IsNullOrEmpty(IsOffensiveText) ||
                    !string.IsNullOrEmpty(IsNotADogOrCatText) ||
                    !string.IsNullOrEmpty(ProblemWithUserText)) &&
                    !string.IsNullOrEmpty(Description);
        }           

        private async void CloseCommandExecute()
        {
            await _navigationService.GoBackAsync(null, true);
        }

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            try
            {
                PosterId = parameters.GetValue<int>("posterId");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
            }
        }
    }
}
