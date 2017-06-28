using Adopcat.Mobile.Models;
using Adopcat.Mobile.ViewModels;
using Xamarin.Forms;

namespace Adopcat.Mobile.Views
{
    public partial class PostersPage : ContentPage
    {
        private PostersPageViewModel ViewModel
        {
            get { return (BindingContext as PostersPageViewModel); }
        }

        public PostersPage()
        {
            InitializeComponent();            
        }

        private void CarouselView_PositionSelected(object sender, SelectedPositionChangedEventArgs e)
        {
            var selectedPoster = carouselView.Item as PosterOutput;
            
            if (selectedPoster == null) return;
            ViewModel.ShowCarouselArrowsCommand.Execute(selectedPoster);

            imgBackArrow.IsVisible = ViewModel.IsNotFirstItem;
            imgForwardArrow.IsVisible = ViewModel.IsNotLastItem;
        }

        private void imgBackArrow_Tapped(object sender, System.EventArgs e)
        {
            carouselView.Position += -1;
        }

        private void imgForwardArrow_Tapped(object sender, System.EventArgs e)
        {
            carouselView.Position += 1;
        }
    }
}
