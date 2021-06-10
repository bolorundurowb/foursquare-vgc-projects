using System;
using neophyte.DataAccess.Implementations;
using neophyte.Models.View;
using neophyte.Views.Auth;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace neophyte.Views.General
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        private readonly TokenClient _tokenClient;
        
        public SettingsPage()
        {
            InitializeComponent();
            _tokenClient = new TokenClient();
        }

        protected override void OnAppearing()
        {
            BindingContext = new SettingsViewModel
            {
                EmailAddress = _tokenClient.GetEmail(),
                LastLogin = _tokenClient.GetLogin().ToLongDateString(),
                LoginExpiresAt = _tokenClient.GetExpiry().ToLongDateString()
            };
        }

        protected void Logout(object sender, EventArgs e)
        {
            _tokenClient.Logout();
            Application.Current.MainPage = new NavigationPage(new SignIn());
        }

        protected async void GoBack(object sender, EventArgs e)
        {
            await Navigation.PopAsync(true);
        }
    }
}
