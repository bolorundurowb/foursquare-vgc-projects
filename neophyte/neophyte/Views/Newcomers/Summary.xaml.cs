﻿using System;
using System.Net;
using System.Threading.Tasks;
using neophyte.DataAccess.Implementations;
using neophyte.Models.View;
using neophyte.Services.Implementations;
using neophyte.Views.Auth;
using neophyte.Views.General;
using Refit;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace neophyte.Views.Newcomers
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewcomersDateSummariesPage : ContentPage
    {
        private readonly NewcomerClient _newcomerClient;

        public NewcomersDateSummariesPage()
        {
            InitializeComponent();
            _newcomerClient = new NewcomerClient();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadDateRecords();
        }

        protected async void OpenDateRecordsPage(object sender, EventArgs e)
        {
            if (collectionDateEntries.SelectedItem is DateSummaryViewModel summary)
            {
                await Navigation.PushAsync(new NewcomersByDatePage(summary.Date));
            }
        }

        protected async void OpenNewRecordPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RecordNewcomerPage());
        }

        protected async void OpenSettingsPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SettingsPage());
        }

        protected async void GenerateDateReport(object sender, EventArgs e)
        {
            var email = await DisplayPromptAsync("Report", "What email address should the report be sent to?",
                "Send", "Cancel", "e.g john@doe.org  (optional)", keyboard: Keyboard.Email);

            if (email == null)
            {
                ToastService.DisplayInfo("Operation cancelled.");
                return;
            }

            if (((SwipeItemView)sender).BindingContext is DateSummaryViewModel summary)
            {
                await _newcomerClient.SendNewcomersReport(summary.Date, email);
                ToastService.DisplaySuccess("Report successfully generated and sent.");
            }
            else
            {
                ToastService.DisplayError("An error occurred when sending the report.");
            }
        }

        protected async void OnRefresh(object sender, EventArgs e)
        {
            await LoadDateRecords();
        }

        private async Task LoadDateRecords()
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await DisplayAlert("Error", "There were issues retrieving data. Please check your internet connection",
                    "Close");
                return;
            }

            try
            {
                rfsLoading.IsRefreshing = true;
                collectionDateEntries.ItemsSource = await _newcomerClient.GetAll();
            }
            catch (ApiException ex) when (ex.StatusCode == HttpStatusCode.Unauthorized)
            {
                // logout and redirect to login
                new TokenClient().Logout();
                Application.Current.MainPage = new NavigationPage(new SignIn());
            }
            finally
            {
                rfsLoading.IsRefreshing = false;
            }
        }
    }
}
