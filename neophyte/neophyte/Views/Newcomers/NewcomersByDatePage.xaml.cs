using System;
using System.Collections.Generic;
using System.Linq;
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
    public partial class NewcomersByDatePage : ContentPage
    {
        private readonly NewcomerClient _newcomerClient;
        private readonly ReportClient _reportClient;
        private readonly DateTime _date;
        private NewcomerViewModel[] _dateRecords;

        public NewcomersByDatePage(DateTime date)
        {
            InitializeComponent();

            Title = "Records";
            SetValue(NavigationPage.BarBackgroundColorProperty, Color.FromHex("#52004C"));

            _date = date;
            _newcomerClient = new NewcomerClient();
            _reportClient = new ReportClient();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadDateRecords();
            prgLoading.IsVisible = false;
            lstDateRecords.IsVisible = true;
        }

        protected async void DeleteRecord(object sender, EventArgs e)
        {
            var newcomer = (sender as MenuItem)?.CommandParameter as NewcomerViewModel;
            await _newcomerClient.DeleteAttendee(newcomer?.Id);

            // refresh view
            lstDateRecords.ItemsSource = _dateRecords.Where(x => x.Id != newcomer?.Id);

            // notify user
            await DisplayAlert("Success", "Record deleted successfully.", "Ok");
        }

        protected async void ViewPersonRecord(object sender, ItemTappedEventArgs e)
        {
            var newcomer = e.Item as NewcomerViewModel;
            await Navigation.PushAsync(new NewcomerDetailsPage(newcomer));
        }

        protected async void RefreshDateRecords(object sender, EventArgs e)
        {
            await LoadDateRecords();
            lstDateRecords.IsRefreshing = false;
        }

        protected async void GenerateDateReport(object sender, EventArgs e)
        {
            await _reportClient.GenerateReport(_date);
            await DisplayAlert("Success", "Report successfully generated and sent.", "Ok");
        }

        private async Task LoadDateRecords()
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                await DisplayAlert("Error", "There were issues retrieving data. Please check your internet connection",
                    "Close");
                return;
            }

            _dateRecords = await _newcomerClient.GetNewcomersForDate(_date);
            lstDateRecords.ItemsSource = _dateRecords;
        }
    }
}