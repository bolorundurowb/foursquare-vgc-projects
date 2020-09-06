using System;
using System.Collections.Generic;
using System.Linq;
using neophyte.Enums;
using neophyte.Firebase;
using neophyte.Interfaces;
using neophyte.Models;
using Plugin.Connectivity;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace neophyte.Views.Newcomers
{
    public partial class DateRecords : ContentPage
    {
        private readonly RecordService _recordService;
        private readonly DateEntry _dateEntry;
        private List<Record> _dateRecords;

        public DateRecords(DateEntry dateEntry)
        {
            InitializeComponent();

            Title = "Records";
            SetValue(NavigationPage.BarBackgroundColorProperty, Color.FromHex("#52004C"));
            _recordService = new RecordService();
            _dateEntry = dateEntry;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // retrieve data from the database
            _dateRecords = await _recordService.GetRecordByDateAsync(_dateEntry?.Date);
            lstDateRecords.ItemsSource = _dateRecords;

            // disable the loading screen
            prgLoading.IsVisible = false;
            lstDateRecords.IsVisible = true;
        }

        protected async void DeleteRecord(object sender, EventArgs e)
        {
            var date = _dateEntry?.Date;
            var record = (sender as MenuItem)?.CommandParameter as Record;
            await _recordService.DeleteRecordAsync(date, record?.RecordId);

            // refresh view
            lstDateRecords.ItemsSource = _dateRecords.Where(x => x.RecordId != record?.RecordId);

            // notify user
            await DisplayAlert("Success", "Record deleted successfully.", "Ok");
        }

        protected async void ViewPersonRecord(object sender, ItemTappedEventArgs e)
        {
            var record = e.Item as Record;
            await Navigation.PushAsync(new RecordDetail(_dateEntry.Date, record));
        }

        protected async void RefreshDateRecords(object sender, EventArgs e)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                // retrieve data from the database
                _dateRecords = await _recordService.GetRecordByDateAsync(_dateEntry?.Date);
                lstDateRecords.ItemsSource = _dateRecords;

                // disable the loading screen
                prgLoading.IsVisible = false;
                lstDateRecords.IsVisible = true;
            }
            else
            {
                const string errorMessage = "We had issues retrieving data. Please check your internet connection";
                await DisplayAlert("Error", errorMessage, "Close");
            }

            lstDateRecords.IsRefreshing = false;
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

            var csvString = await _recordService.GenerateCsvForDateAsync(_dateEntry.Date);
            var filePersistenceHandler = DependencyService.Get<IFilePersistence>();
            var filePath = filePersistenceHandler.SaveFile(_dateEntry.Date, csvString, RecordType.Newcomers);

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
    }
}
