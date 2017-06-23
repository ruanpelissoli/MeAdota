using Adopcat.Mobile.ViewModels;
using Xamarin.Forms;

namespace Adopcat.Mobile.Views
{
    public partial class MyPostersPage : ContentPage
    {
        private MyPostersPageViewModel ViewModel
        {
            get { return (BindingContext as MyPostersPageViewModel); }
        }

        public MyPostersPage()
        {
            InitializeComponent();
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var listView = (sender as ListView);
            if (listView.SelectedItem == null) return;

            var poster = listView.SelectedItem as Models.PosterOutput;
            ViewModel.SelectedPosterCommand.Execute(poster.Id);

            listView.SelectedItem = null;
        }
    }
}
