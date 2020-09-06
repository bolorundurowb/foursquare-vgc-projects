using System;
using neophyte.Firebase;
using neophyte.Views.Newcomers;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace neophyte.Views.Auth
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignIn : ContentPage
    {
        private readonly AuthService _authService;

        public SignIn()
        {
            InitializeComponent();

            Title = "Sign In";
            _authService = new AuthService();
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
