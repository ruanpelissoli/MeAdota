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

            var castrated = currentPage.Content.FindByName<Image>("imgCastrated");
            var dewormed = currentPage.Content.FindByName<Image>("imgDewormed");

            var selectedPoster = SelectedItem as PosterOutput;

            castrated.Source = selectedPoster.Castrated ? ImageSource.FromFile("icon_checked.png") : ImageSource.FromFile("icon_not_checked.png");
            dewormed.Source = selectedPoster.Dewormed ? ImageSource.FromFile("icon_checked.png") : ImageSource.FromFile("icon_not_checked.png");

            if (selectedPoster == null) return;
            ViewModel.ShowCarouselArrowsCommand.Execute(selectedPoster);

            backArrow.IsVisible = ViewModel.IsNotFirstItem;
            frontArrow.IsVisible = ViewModel.IsNotLastItem;
        }
    }
}
