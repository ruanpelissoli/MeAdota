using Adopcat.Mobile.ViewModels;
using Xamarin.Forms;

namespace Adopcat.Mobile.Views
{
    public partial class ReportPosterPage : ContentPage
    {
        private ReportPosterPageViewModel ViewModel
        {
            get { return (BindingContext as ReportPosterPageViewModel); }
        }

        public ReportPosterPage()
        {
            InitializeComponent();
        }

        private void IsOffensive_Toggled(object sender, ToggledEventArgs e)
        {
            var swcIsOffensive = (sender as Switch);

            if (swcIsOffensive == null) return;

            if (swcIsOffensive.IsToggled)
                ViewModel.IsOffensiveText = "Achei essa publicação ofensiva.";
            else
                ViewModel.IsOffensiveText = string.Empty;

        }

        private void IsNotADogOrCat_Toggled(object sender, ToggledEventArgs e)
        {
            var swcIsNotADogOrCat = (sender as Switch);

            if (swcIsNotADogOrCat == null) return;

            if (swcIsNotADogOrCat.IsToggled)
                ViewModel.IsNotADogOrCatText = "Essa publicação não é nem de um gato nem de um cachorro.";
            else
                ViewModel.IsNotADogOrCatText = string.Empty;
        }

        private void ProblemWithUser_Toggled(object sender, ToggledEventArgs e)
        {
            var swcProblemWithUser = (sender as Switch);

            if (swcProblemWithUser == null) return;

            if (swcProblemWithUser.IsToggled)
                ViewModel.ProblemWithUserText = "Tive problemas com o usuário que criou a publicação.";
            else
                ViewModel.ProblemWithUserText = string.Empty;
        }
    }
}
