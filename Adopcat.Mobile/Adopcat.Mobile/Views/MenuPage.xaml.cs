using Adopcat.Mobile.ViewModels;
using Xamarin.Forms;

namespace Adopcat.Mobile.Views
{
    public partial class MenuPage : MasterDetailPage
    {
        private MenuPageViewModel ViewModel
        {
            get
            {
                return (BindingContext as MenuPageViewModel);
            }
        }

        public MenuPage()
        {
            InitializeComponent();
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var listView = (sender as ListView);
            if (listView.SelectedItem == null) return;
            var menuItem = listView.SelectedItem as Models.MenuItem;
            
            ViewModel.MenuClickedCommand.Execute(menuItem.GoTo);            

            listView.SelectedItem = null;
        }
    }
}