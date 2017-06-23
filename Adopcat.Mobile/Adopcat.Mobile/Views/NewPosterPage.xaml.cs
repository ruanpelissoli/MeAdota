using Adopcat.Mobile.ViewModels;
using System.Collections.Specialized;
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

            ViewModel.PetImages.CollectionChanged += PetImagesCollectionChangedEvent;
        }

        private void PetImagesCollectionChangedEvent(object sender, NotifyCollectionChangedEventArgs e)
        {
            lsvPetImages.HeightRequest = (ViewModel.PetImages.Count * 100);
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

        private void Picker_SelectedIndexChanged_1(object sender, System.EventArgs e)
        {
            var selectedItem = (sender as Picker).SelectedItem as string;
            ViewModel.StateSelectCommand.Execute(selectedItem);
        }
    }
}
