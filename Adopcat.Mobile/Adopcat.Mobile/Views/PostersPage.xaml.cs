using Adopcat.Mobile.Models;
using Adopcat.Mobile.ViewModels;
using Xamarin.Forms;

namespace Adopcat.Mobile.Views
{
    public partial class PostersPage : CarouselPage
    {
        private PostersPageViewModel ViewModel
        {
            get { return (BindingContext as PostersPageViewModel); }
        }

        public PostersPage()
        {
            InitializeComponent();
        }

        protected override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();

            var currentPage = CurrentPage;

            if (currentPage == null) return;
            var backArrow = currentPage.Content.FindByName<Image>("imgBackArrow");
            var frontArrow = currentPage.Content.FindByName<Image>("imgForwardArrow");

            var selectedPoster = SelectedItem as PosterOutput;

            if (selectedPoster == null) return;
            ViewModel.ShowCarouselArrowsCommand.Execute(selectedPoster);

            backArrow.IsVisible = ViewModel.IsNotFirstItem;
            frontArrow.IsVisible = ViewModel.IsNotLastItem;
        }
    }
}
