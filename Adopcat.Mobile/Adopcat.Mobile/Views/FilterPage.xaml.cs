using Adopcat.Mobile.ViewModels;
using Xamarin.Forms;

namespace Adopcat.Mobile.Views
{
    public partial class FilterPage : ContentPage
    {
        public FilterPageViewModel ViewModel
        {
            get { return (BindingContext as FilterPageViewModel); }
        }

        public FilterPage()
        {
            InitializeComponent();
        }

        private void Picker_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            var selectedItem = (sender as Picker).SelectedItem as string;
            ViewModel.PetTypeSelectCommand.Execute(selectedItem);
        }
    }
}
