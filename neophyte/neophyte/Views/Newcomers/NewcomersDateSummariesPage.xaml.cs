using System;
using System.Threading.Tasks;
using neophyte.DataAccess.Implementations;
using neophyte.Enums;
using neophyte.Firebase;
using neophyte.Interfaces;
using neophyte.Models;
using neophyte.Models.View;
using Plugin.Connectivity;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace neophyte.Views.Newcomers
{
    public partial class NewcomersDateSummariesPage : ContentPage
    {
        private readonly NewcomerClient _newcomerClient;
        private readonly ReportClient _reportClient;

        public NewcomersDateSummariesPage()
        {
            InitializeComponent();

            Title = "Newcomers";
            SetValue(NavigationPage.BarBackgroundColorProperty, Color.FromHex("#52004C"));

            if (Device.RuntimePlatform == Device.iOS)
            {
                btnAddRecord.TextColor = Color.Black;
            }

            _newcomerClient = new NewcomerClient();
            _reportClient = new ReportClient();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadDateRecords();
            prgLoading.IsVisible = false;
            lstDateEntries.IsVisible = true;
        }

        protected async void OpenDateRecordsPage(object sender, ItemTappedEventArgs e)
        {
            var summary = e.Item as DateSummaryViewModel;
            await Navigation.PushAsync(new NewcomersByDatePage(summary.Date));
        }

        protected async void OpenNewRecordPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RecordNewcomerPage());
        }

        protected async void GenerateDateReport(object sender, EventArgs e)
        {
            if ((sender as MenuItem)?.CommandParameter is DateSummaryViewModel summary)
            {
                await _reportClient.GenerateReport(summary.Date);
            }

            await DisplayAlert("Success", "Report successfully generated and sent.", "Ok");
        }

        protected async void RefreshDateRecords(object sender, EventArgs e)
        {
            await LoadDateRecords();
            lstDateEntries.IsRefreshing = false;
        }

        private async Task LoadDateRecords()
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                await DisplayAlert("Error", "There were issues retrieving data. Please check your internet connection",
                    "Close");
                return;
            }

            lstDateEntries.ItemsSource = await _newcomerClient.GetAll();
        }
    }
}