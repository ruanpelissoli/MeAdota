﻿using System;

using Android.App;
using Android.Content.PM;
using Android.OS;
using Prism.Unity;
using Microsoft.Practices.Unity;
using PCLAppConfig;
using Plugin.Permissions;

namespace Adopcat.Mobile.Droid
{
    [Activity(Label = "Adopcat.Mobile", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        static MainActivity instance = null;
        public static MainActivity CurrentActivity
        {
            get
            {
                return instance;
            }
        }

        protected override void OnCreate(Bundle bundle)
        {
            instance = this;

            TabLayoutResource = Resource.Layout.tabs;
            ToolbarResource = Resource.Layout.toolbar;

            base.OnCreate(bundle);

            AppDomain.CurrentDomain.UnhandledException += (sender, unhandledExceptionEventArgs) =>
            {
                Console.WriteLine(new Exception("UnhandledException", unhandledExceptionEventArgs.ExceptionObject as Exception).StackTrace);
            };

            global::Xamarin.Forms.Forms.Init(this, bundle);
            ConfigurationManager.Initialise(PCLAppConfig.FileSystemStream.PortableStream.Current);
            Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();
            LoadApplication(new App(new AndroidInitializer()));
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }

    public class AndroidInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IUnityContainer container)
        {

        }
    }
}
