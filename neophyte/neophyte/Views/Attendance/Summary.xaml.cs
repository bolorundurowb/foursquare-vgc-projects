using System;
using System.Net;
using System.Threading.Tasks;
using MongoDB.Bson;
using neophyte.DataAccess.Implementations;
using neophyte.Models.View;
using neophyte.Services.Implementations;
using neophyte.Services.Interfaces;
using neophyte.Views.Auth;
using neophyte.Views.General;
using neophyte.Views.Modals;
using Refit;
using Xamarin.CommunityToolkit.Extensions;
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
        }

        protected async void OpenDateRecordsPage(object sender, EventArgs e)
        {
            if (collectionDateEntries.SelectedItem is DateSummaryViewModel summary)
            {
                await Navigation.PushAsync(new AttendanceByDatePage(summary.Date));
            }
        }

        protected async void OpenScanner(object sender, EventArgs e)
        {
            // var scanner = DependencyService.Get<IQrScanService>();
            // var result = await scanner.ScanAsync();
            var result = "61740f9c317188ac70fbb9de";

            if (string.IsNullOrWhiteSpace(result))
            {
                ToastService.DisplayInfo("Scan unsuccessful.");
                return;
            }

            // verify it is an object id
            if (!ObjectId.TryParse(result, out _))
            {
                ToastService.DisplayError("Invalid scan result. Try again!");
                return;
            }

            var popup = new AttendanceRegistration(result);
            await Navigation.ShowPopupAsync(popup);
        }

        protected async void OpenSettingsPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SettingsPage());
        }

        protected async void GenerateDateReport(object sender, EventArgs e)
        {
            var email = await DisplayPromptAsync("Report", "What email address should the report be sent to?",
                "Send", "Cancel", "e.g john@doe.org (optional)", keyboard: Keyboard.Email);

            if (email == null)
            {
                ToastService.DisplayInfo("Operation cancelled.");
                return;
            }

            if (((SwipeItemView)sender).BindingContext is DateSummaryViewModel summary)
            {
                await _attendanceClient.SendAttendanceReport(summary.Date, email);
                ToastService.DisplaySuccess("Report successfully generated and sent.");
            }
            else
            {
                ToastService.DisplayError("An error occurred when sending the report.");
            }
        }

        protected async void OnRefresh(object sender, EventArgs e)
        {
            await LoadDateRecords();
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
                rfsLoading.IsRefreshing = true;
                collectionDateEntries.ItemsSource = await _attendanceClient.GetAll();
            }
            catch (ApiException ex) when (ex.StatusCode == HttpStatusCode.Unauthorized)
            {
                // logout and redirect to login
                new TokenClient().Logout();
                Application.Current.MainPage = new NavigationPage(new SignIn());
            }
            finally
            {
                rfsLoading.IsRefreshing = false;
            }
        }
    }
}
