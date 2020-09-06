using System;
using System.Threading.Tasks;
using neophyte.Enums;
using neophyte.Firebase;
using neophyte.Interfaces;
using neophyte.Models;
using Plugin.Connectivity;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace neophyte.Views.Newcomers
{
    public partial class HomePage : ContentPage
    {
        private readonly RecordService _recordService;

        public HomePage()
        {
            InitializeComponent();

            Title = "Newcomers";
            SetValue(NavigationPage.BarBackgroundColorProperty, Color.FromHex("#52004C"));
            _recordService = new RecordService();

            if (Device.RuntimePlatform == Device.iOS)
            {
                btnAddRecord.TextColor = Color.Black;
            }
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
            var dateEntry = e.Item as DateEntry;
            await Navigation.PushAsync(new DateRecords(dateEntry));
        }

        protected async void OpenNewRecordPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewRecord());
        }

        protected async void GenerateDateReport(object sender, EventArgs e)
        {
            var status = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();
            if (status != PermissionStatus.Granted)
            {
                status = await Permissions.RequestAsync<Permissions.StorageWrite>();
            }

            if (status != PermissionStatus.Granted)
            {
                return;
            }

            var dateEntry = (sender as MenuItem)?.CommandParameter as DateEntry;
            var csvString = await _recordService.GenerateCsvForDateAsync(dateEntry?.Date);
            var filePersistenceHandler = DependencyService.Get<IFilePersistence>();
            var filePath = filePersistenceHandler.SaveFile(dateEntry?.Date, csvString, RecordType.Newcomers);

            // let the user know
            await DisplayAlert("Success", "Report successfully generated.", "Ok");

            // share the file if iOS as it is harder to access the file system
            if (Device.RuntimePlatform == Device.iOS)
            {
                await Share.RequestAsync(new ShareFileRequest
                {
                    Title = "Share report",
                    File = new ShareFile(filePath)
                });
            }
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

            lstDateEntries.ItemsSource = await _recordService.GetDayEntriesAsync();
        }
    }
}
