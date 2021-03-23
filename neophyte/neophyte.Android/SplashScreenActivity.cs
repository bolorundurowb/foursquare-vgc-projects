using Android.App;
using Android.Content;
using Android.Content.PM;
using AndroidX.AppCompat.App;

namespace neophyte.Droid
{
    [Activity(Theme = "@style/MainTheme.Splash", MainLauncher = true, NoHistory = true, 
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class SplashScreenActivity : AppCompatActivity
    {
        protected override void OnResume()
        {
            base.OnResume();
            StartActivity(new Intent(Application.Context, typeof (MainActivity)));
        }
        
        public override void OnBackPressed() { }
    }
}
