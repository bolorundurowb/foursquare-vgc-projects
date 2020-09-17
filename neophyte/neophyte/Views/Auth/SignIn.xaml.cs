using System;
using neophyte.DataAccess.Implementations;
using Refit;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace neophyte.Views.Auth
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignIn : ContentPage
    {
        private readonly AuthClient _authClient;

        public SignIn()
        {
            InitializeComponent();

            Title = "Sign In";
            _authClient = new AuthClient();
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

            try
            {
                var response = await _authClient.Login(email);
                _authClient.SetAuth(response.Token, response.ExpiresAt);

                // send to home page
                Navigation.InsertPageBefore(new RootPage(), this);
                await Navigation.PopAsync();
            }
            catch (ApiException)
            {
                txtEmail.IsEnabled = true;
                btnLogin.IsVisible = true;
                prgLoading.IsVisible = false;

                await DisplayAlert("Error", "Sorry you cannot access this app at this time.", "Ok");
            }
        }
    }
}