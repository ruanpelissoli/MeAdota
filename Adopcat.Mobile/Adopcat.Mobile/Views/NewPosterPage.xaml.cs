using Adopcat.Mobile.ViewModels;
using Xamarin.Forms;

namespace Adopcat.Mobile.Views
{
    public partial class NewPosterPage : ContentPage
    {
        public NewPosterPageViewModel ViewModel
        {
            get { return (BindingContext as NewPosterPageViewModel); }
        }

        public NewPosterPage()
        {
            InitializeComponent();
        }

        private void Picker_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            var selectedItem = (sender as Picker).SelectedItem as string;
        }
    }
}
