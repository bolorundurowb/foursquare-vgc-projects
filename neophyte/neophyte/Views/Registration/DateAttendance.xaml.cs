using System;
using System.Linq;
using System.Threading.Tasks;
using neophyte.DataAccess.Implementations;
using neophyte.Models.View;
using Plugin.Connectivity;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace neophyte.Views.Registration
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DateAttendance : ContentPage
    {
        private readonly AttendanceClient _attendanceClient;
        private readonly ReportClient _reportClient;
        private readonly DateTime _date;
        private AttendeeViewModel[] _dateRecords;

        public DateAttendance(DateTime date)
        {
            InitializeComponent();

            Title = "Entries";
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
            await Navigation.PushAsync(new AttendanceDetails(attendee));
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

            _dateRecords = await _attendanceClient.GetAttendanceForDate(_date);
            lstDateRecords.ItemsSource = _dateRecords;
        }
    }
}
