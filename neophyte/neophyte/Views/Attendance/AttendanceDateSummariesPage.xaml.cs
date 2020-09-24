using System;
using System.Threading.Tasks;
using neophyte.DataAccess.Implementations;
using neophyte.Models.View;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace neophyte.Views.Attendance
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AttendanceDateSummariesPage : ContentPage
    {
        private readonly AttendanceClient _attendanceClient;
        private readonly ReportClient _reportClient;

        public AttendanceDateSummariesPage()
        {
            InitializeComponent();

            Title = "Attendance";
            SetValue(NavigationPage.BarBackgroundColorProperty, Color.FromHex("#52004C"));

            if (Device.RuntimePlatform == Device.iOS)
            {
                btnAddRecord.TextColor = Color.Black;
            }

            _attendanceClient = new AttendanceClient();
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
            await Navigation.PushAsync(new AttendanceByDatePage(summary.Date));
        }

        protected async void OpenNewRecordPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterAttendeePage());
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
            lstDateEntries.IsRefreshing = false;
        }

        private async Task LoadDateRecords()
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await DisplayAlert("Error", "There were issues retrieving data. Please check your internet connection",
                    "Close");
                return;
            }

            lstDateEntries.ItemsSource = await _attendanceClient.GetAll();
        }
    }
}