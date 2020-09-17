using neophyte.DataAccess.Implementations;
using neophyte.Views;
using neophyte.Views.Auth;
using Xamarin.Forms;

[assembly: ExportFont("FA-Brands.otf", Alias = "FAB")]
[assembly: ExportFont("FA-Regular.otf", Alias = "FAR")]
[assembly: ExportFont("FA-Solid.otf", Alias = "FAS")]
namespace neophyte
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // determine page to navigate to
            NavigationPage mainPage;
            var isLoggedIn = new AuthClient().IsLoggedIn();

            if (isLoggedIn)
            {
                mainPage = new NavigationPage(new RootPage())
                {
                    BarBackgroundColor = Color.FromHex("#92278F")
                };
            }
            else
            {
                mainPage = new NavigationPage(new SignIn())
                {
                    BarBackgroundColor = Color.FromHex("#92278F")
                };
            }

            MainPage = mainPage;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
