﻿using Adopcat.Mobile.ViewModels;
using Xamarin.Forms;

namespace Adopcat.Mobile.Views
{
    public partial class EditMyPosterPage : ContentPage
    {
        public EditMyPosterPageViewModel ViewModel
        {
            get { return (BindingContext as EditMyPosterPageViewModel); }
        }

        public EditMyPosterPage()
        {
            InitializeComponent();
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var list = (sender as ListView);
            if (list.SelectedItem == null) return;
            list.SelectedItem = null;
        }

        private void Picker_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            var selectedItem = (sender as Picker).SelectedItem as string;
            ViewModel.PetTypeSelectCommand.Execute(selectedItem);
        }

        private void Picker_SelectedIndexChanged_1(object sender, System.EventArgs e)
        {
            var selectedItem = (sender as Picker).SelectedItem as string;
            ViewModel.StateSelectCommand.Execute(selectedItem);
        }
    }
}