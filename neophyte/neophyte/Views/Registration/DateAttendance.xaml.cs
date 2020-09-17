using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using Xamarin.Forms.Xaml;

namespace neophyte.Views.Registration
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DateAttendance : ContentPage
    {
        private readonly AttendanceClient _attendanceClient;
        private readonly DateTime _date;
        private AttendeeViewModel[] _dateRecords;

        public DateAttendance(DateTime date)
        {
            InitializeComponent();

            Title = "Entries";
            SetValue(NavigationPage.BarBackgroundColorProperty, Color.FromHex("#52004C"));
            
            _date = date;
            _attendanceClient = new AttendanceClient();
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
            // await _attendanceService.DeleteAttendanceAsync(date, attendance?.AttendanceId);

            // refresh view
            lstDateRecords.ItemsSource = _dateRecords.Where(x => x.Id != attendance?.Id);

            // notify user
            await DisplayAlert("Success", "Record deleted successfully.", "Ok");
        }

        protected async void ViewAttendanceDetail(object sender, ItemTappedEventArgs e)
        {
            var attendance = e.Item as Attendance;
            await Navigation.PushAsync(new AttendanceDetails(attendance));
        }

        protected async void RefreshDateRecords(object sender, EventArgs e)
        {
            await LoadDateRecords();
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

            // var csvString = await _attendanceService.GenerateCsvForDateAsync(_dateEntry.Date);
            // var filePersistenceHandler = DependencyService.Get<IFilePersistence>();
            // var filePath = filePersistenceHandler.SaveFile(_dateEntry.Date, csvString, RecordType.Attendance);
            //
            // // let the user know
            // await DisplayAlert("Success", "Report successfully generated.", "Ok");
            //
            // // share the file if iOS as it is harder to access the file system
            // if (Device.RuntimePlatform == Device.iOS)
            // {
            //     await Share.RequestAsync(new ShareFileRequest
            //     {
            //         Title = "Share report",
            //         File = new ShareFile(filePath)
            //     });
            // }
        }

        private async Task LoadDateRecords()
        {
            if (!CrossConnectivity.Current.IsConnected)
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
