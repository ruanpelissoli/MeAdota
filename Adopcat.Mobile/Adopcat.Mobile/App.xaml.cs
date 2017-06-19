﻿using Prism.Unity;
using Adopcat.Mobile.Views;
using Xamarin.Forms;
using Adopcat.Mobile.Util;
using Adopcat.Mobile.Interfaces;
using Refit;
using System;
using System.Diagnostics;
using Adopcat.Mobile.ViewModels;
using Adopcat.Mobile.Helpers;
using Adopcat.Mobile.Services;

namespace Adopcat.Mobile
{
    public partial class App : PrismApplication
    {
        public static IApiService ApiService { get; private set; }
        public static AzureService MobileService { get; private set; }

        public App(IPlatformInitializer initializer = null) : base(initializer) { }

        protected override void OnInitialized()
        {
            try
            {
                InitializeComponent();

                MobileService = DependencyService.Get<AzureService>();
                MobileService.Initialize();

                ApiService = RestService.For<IApiService>(ApplicationParameters.ApiUrl);
                
                if (!Settings.IsLoggedIn)
                    NavigationService.NavigateAsync($"NavigationPage/{nameof(LoginPage)}");
                else
                    NavigationService.NavigateAsync($"{nameof(MenuPage)}/NavigationPage/{nameof(PostersPage)}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
            }
        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<NavigationPage>();
            Container.RegisterTypeForNavigation<MenuPage, MenuPageViewModel>();
            Container.RegisterTypeForNavigation<LoginPage, LoginPageViewModel>();
            Container.RegisterTypeForNavigation<LoginDataPage, LoginDataPageViewModel>();
            Container.RegisterTypeForNavigation<RegisterPage, RegisterPageViewModel>();
            Container.RegisterTypeForNavigation<PostersPage, PostersPageViewModel>();
            Container.RegisterTypeForNavigation<NewPosterPage, NewPosterPageViewModel>();
            Container.RegisterTypeForNavigation<ConfigurationPage, ConfigurationPageViewModel>();
            Container.RegisterTypeForNavigation<MyPostersPage, MyPostersPageViewModel>();
            Container.RegisterTypeForNavigation<FilterPage, FilterPageViewModel>();
            Container.RegisterTypeForNavigation<PosterDetailPage>();
        }
    }
}