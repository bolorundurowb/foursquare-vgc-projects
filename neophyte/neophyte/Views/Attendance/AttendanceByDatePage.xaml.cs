using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using neophyte.DataAccess.Implementations;
using neophyte.Models.View;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace neophyte.Views.Attendance
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AttendanceByDatePage : ContentPage
    {
        private readonly AttendanceClient _attendanceClient;
        private readonly ReportClient _reportClient;
        private readonly DateTime _date;
        private AttendeeViewModel[] _dateRecords;

        public AttendanceByDatePage(DateTime date)
        {
            InitializeComponent();

            Title = "Attendees";
            SetValue(NavigationPage.BarBackgroundColorProperty, Color.FromHex("#52004C"));

            _date = date;
            _attendanceClient = new AttendanceClient();
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
            var attendance = (sender as MenuItem)?.CommandParameter as AttendeeViewModel;
            await _attendanceClient.DeleteAttendee(attendance?.Id);

            // refresh view
            lstDateRecords.ItemsSource = _dateRecords.Where(x => x.Id != attendance?.Id);

            // notify user
            await DisplayAlert("Success", "Record deleted successfully.", "Ok");
        }

        protected async void ViewAttendanceDetail(object sender, ItemTappedEventArgs e)
        {
            var attendee = e.Item as AttendeeViewModel;
            await Navigation.PushAsync(new AttendeeDetailsPage(attendee));
        }

        protected async void RefreshDateRecords(object sender, EventArgs e)
        {
            await LoadDateRecords();
            lstDateRecords.IsRefreshing = false;
        }

        protected async void GenerateDateReport(object sender, EventArgs e)
        {
            await _reportClient.GenerateReport(_date);
            await DisplayAlert("Success", "Report successfully generated and sent.", "Okay");
        }

        protected void SearchAttendance(object sender, TextChangedEventArgs e)
        {
            var query = e.NewTextValue?.ToLowerInvariant() ?? string.Empty;
            var results = new List<AttendeeViewModel>();
            foreach (var dateRecord in _dateRecords)
            {
                if (dateRecord.FullName?.ToLowerInvariant().Contains(query) == true)
                {
                    results.Add(dateRecord);
                    continue;
                }
                
                if (dateRecord.EmailAddress?.ToLowerInvariant().Contains(query) == true)
                {
                    results.Add(dateRecord);
                }
            }

            lstDateRecords.ItemsSource = results;
        }

        private async Task LoadDateRecords()
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await DisplayAlert("Error", "There were issues retrieving data. Please check your internet connection",
                    "Close");
                return;
            }

            _dateRecords = await _attendanceClient.GetAttendanceForDate(_date);
            lstDateRecords.ItemsSource = _dateRecords;
        }
    }
}