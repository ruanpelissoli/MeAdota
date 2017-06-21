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
            ViewModel.PetTypeSelectCommand.Execute(selectedItem);
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var list = (sender as ListView);
            if (list.SelectedItem == null) return;
            list.SelectedItem = null;
        }
    }
}
