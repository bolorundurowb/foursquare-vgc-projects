using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using neophyte.DataAccess.Implementations;
using neophyte.Models.View;
using neophyte.Services.Implementations;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace neophyte.Views.Attendance
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AttendanceByDatePage : ContentPage
    {
        private readonly AttendanceClient _attendanceClient;
        private readonly DateTime _date;
        private AttendeeViewModel[] _dateRecords;

        public AttendanceByDatePage(DateTime date)
        {
            InitializeComponent();

            _date = date;
            _attendanceClient = new AttendanceClient();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // set the header
            lblHeader.Text = _date.ToString("d MMM \\'yy.");

            // load page data
            await LoadDateRecords();
            prgLoading.IsVisible = false;
            collectionDateEntries.IsVisible = true;
        }

        protected async void DeleteRecord(object sender, EventArgs e)
        {
            var shouldDelete = await DisplayAlert("Confirm Delete",
                "Are you sure you want to permanently delete this attendee?", "Yes", "No");

            if (shouldDelete)
            {
                if (((SwipeItemView) sender).BindingContext is AttendeeViewModel attendee)
                {
                    await _attendanceClient.DeleteAttendee(attendee.Id);

                    // refresh view
                    collectionDateEntries.ItemsSource = _dateRecords.Where(x => x.Id != attendee.Id);

                    // notify user
                    ToastService.DisplaySuccess("Entry successfully removed.");
                }
                else
                {
                    ToastService.DisplayError("An error occurred when deleting the entry.");
                }
            }
            else
            {
                ToastService.DisplayInfo("Operation cancelled.");
            }
        }

        protected async void EditRecord(object sender, EventArgs e)
        {
            if (((SwipeItemView) sender).BindingContext is AttendeeViewModel attendee)
            {
                await Navigation.PushAsync(new AttendeeDetailsPage(attendee, true));
            }
        }

        protected async void ViewAttendanceDetail(object sender, EventArgs e)
        {
            if (collectionDateEntries.SelectedItem is AttendeeViewModel attendee)
            {
                await Navigation.PushAsync(new AttendeeDetailsPage(attendee));
            }
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

            collectionDateEntries.ItemsSource = results;
        }

        protected async void GoBack(object sender, EventArgs e)
        {
            await Navigation.PopAsync(true);
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
            collectionDateEntries.ItemsSource = _dateRecords;
        }
    }
}
