using System;
using neophyte.DataAccess.Interfaces;
using neophyte.Firebase;
using neophyte.Views.Newcomers;
using Refit;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace neophyte.Views.Auth
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignIn : ContentPage
    {
        private readonly IAuthClient _authClient;

        public SignIn()
        {
            InitializeComponent();

            Title = "Sign In";
            _authClient = RestService.For<IAuthClient>(Constants.BaseUrl);
        }

        protected async void Login(object sender, EventArgs e)
        {
            var email = txtEmail.Text;

            if (string.IsNullOrWhiteSpace(email))
            {
                await DisplayAlert("Error", "The email address field is required.", "Ok");
                return;
            }

            txtEmail.IsEnabled = false;
            btnLogin.IsVisible = false;
            prgLoading.IsVisible = true;

            var authResult = await _authService.VerifyAdminEmail(email);
            if (authResult)
            {
                Preferences.Set(AuthService.AuthKey, true);
                // send to home page
                Navigation.InsertPageBefore(new RootPage(), this);
                await Navigation.PopAsync();
            }
            else
            {
                txtEmail.IsEnabled = true;
                btnLogin.IsVisible = true;
                prgLoading.IsVisible = false;

                await DisplayAlert("Error", "You are not authorized to access this app.", "Ok");
            }
        }
    }
}
