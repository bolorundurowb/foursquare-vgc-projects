using Acr.UserDialogs;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using GoogleVisionBarCodeScanner.Droid;
using Rg.Plugins.Popup;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Config = Plugin.InputKit.Platforms.Droid.Config;
using Platform = Xamarin.Essentials.Platform;

namespace neophyte.Droid
{
    [Activity(Label = "Neophyte", Icon = "@mipmap/ic_launcher", Theme = "@style/MainTheme",
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            RendererInitializer.Init();
            Platform.Init(this, savedInstanceState);
            Popup.Init(this);
            Forms.SetFlags("SwipeView_Experimental");
            Forms.Init(this, savedInstanceState);
            Config.Init(this, savedInstanceState);
            UserDialogs.Init(this);
            LoadApplication(new App());

            Xamarin.Forms.Application.Current.On<Xamarin.Forms.PlatformConfiguration.Android>()
                .UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Resize);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions,
            [GeneratedEnum] Permission[] grantResults)
        {
            Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public override void OnBackPressed()
        {
            Popup.SendBackPressed(base.OnBackPressed);
        }
    }
}
