using System;
using System.Net;
using System.Threading.Tasks;
using neophyte.DataAccess.Implementations;
using neophyte.Models.View;
using neophyte.Utils;
using neophyte.Views.Auth;
using Refit;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace neophyte.Views.Attendance
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AttendanceDateSummariesPage : ContentPage
    {
        private readonly AttendanceClient _attendanceClient;

        public AttendanceDateSummariesPage()
        {
            InitializeComponent();
            _attendanceClient = new AttendanceClient();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadDateRecords();
            prgLoading.IsVisible = false;
            collectionDateEntries.IsVisible = true;
        }

        protected async void OpenDateRecordsPage(object sender, EventArgs e)
        {
            if (collectionDateEntries.SelectedItem is DateSummaryViewModel summary)
            {
                await Navigation.PushAsync(new AttendanceByDatePage(summary.Date));
            }
        }

        protected async void OpenNewRecordPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterAttendeePage());
        }

        protected async void GenerateDateReport(object sender, EventArgs e)
        {
            var email = await DisplayPromptAsync("Report", "What email address should the report be sent to?",
                "Send", "Cancel", "e.g john@doe.org (optional)", keyboard: Keyboard.Email);

            if (email == null)
            {
                Toasts.DisplayInfo("Operation cancelled.");
                return;
            }

            if (((SwipeItemView) sender).BindingContext is DateSummaryViewModel summary)
            {
                await _attendanceClient.SendAttendanceReport(summary.Date, email);
                Toasts.DisplaySuccess("Report successfully generated and sent.");
            }
            else
            {
                Toasts.DisplayError("An error occurred when sending the report.");
            }
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
                collectionDateEntries.ItemsSource = await _attendanceClient.GetAll();
            }
            catch (ApiException ex) when (ex.StatusCode == HttpStatusCode.Unauthorized)
            {
                // logout and redirect to login
                new TokenClient().Logout();
                Application.Current.MainPage = new NavigationPage(new SignIn());
            }
        }
    }
}
