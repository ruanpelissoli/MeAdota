using Adopcat.Mobile.ViewModels;
using Xamarin.Forms;

namespace Adopcat.Mobile.Views
{
    public partial class PosterDetailPage : ContentPage
    {
        private PosterDetailPageViewModel ViewModel
        {
            get { return (BindingContext as PosterDetailPageViewModel); }
        }

        public PosterDetailPage()
        {
            InitializeComponent();
        }

        private void CarouselView_PositionSelected(object sender, SelectedPositionChangedEventArgs e)
        {
            ViewModel.Position = (sender as CarouselView).Position;
        }        
    }
}
