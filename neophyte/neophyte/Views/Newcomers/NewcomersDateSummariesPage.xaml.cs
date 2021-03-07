using System;
using System.Threading.Tasks;
using neophyte.DataAccess.Implementations;
using neophyte.Models.View;
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
            collectionDateEntries.IsVisible = true;
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
            var email = await DisplayPromptAsync("Report", "What email address should the report be sent to?",
                "Generate", "Cancel", "e.g john@doe.org", keyboard: Keyboard.Email);

            if (string.IsNullOrWhiteSpace(email))
            {
                return;
            }

            if ((sender as MenuItem)?.CommandParameter is DateSummaryViewModel summary)
            {
                await _reportClient.GenerateReport(summary.Date, email);
            }

            await DisplayAlert("Success", "Report successfully generated and sent.", "Ok");
        }

        protected async void RefreshDateRecords(object sender, EventArgs e)
        {
            await LoadDateRecords();
            // collectionDateEntries.IsRefreshing = false;
        }

        private async Task LoadDateRecords()
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await DisplayAlert("Error", "There were issues retrieving data. Please check your internet connection",
                    "Close");
                return;
            }

            collectionDateEntries.ItemsSource = await _newcomerClient.GetAll();
        }
    }
}
