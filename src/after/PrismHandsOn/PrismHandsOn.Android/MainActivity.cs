using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Prism;
using Prism.Ioc;
using PrismHandsOn.Models;

namespace PrismHandsOn.Droid
{
    [Activity(Label = "PrismHandsOn", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App(new PlatformInitializer(this)));
        }

        private class PlatformInitializer : IPlatformInitializer
        {
            private readonly Context _context;

            public PlatformInitializer(Context context)
            {
                _context = context;
            }

            public void RegisterTypes(IContainerRegistry containerRegistry)
            {
                containerRegistry.Register<ITextToSpeechService, TextToSpeechService>();
                containerRegistry.RegisterInstance(_context);
            }
        }
    }
}

