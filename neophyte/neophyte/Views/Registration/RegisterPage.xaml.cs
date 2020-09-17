using System;
using System.Linq;
using System.Threading.Tasks;
using neophyte.DataAccess.Implementations;
using neophyte.Firebase;
using neophyte.Models;
using neophyte.Models.Binding;
using neophyte.Validators;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace neophyte.Views.Registration
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        private readonly AttendanceClient _attendanceClient;
        private readonly AttendanceValidator _attendanceValidator = new AttendanceValidator();

        public RegisterPage()
        {
            InitializeComponent();

            Title = "Register";
            SetValue(NavigationPage.BarBackgroundColorProperty, Color.FromHex("#52004C"));

            // set drop down values
            cmbGender.ItemsSource = Constants.Genders;

            // initialize stuff
            _attendanceClient = new AttendanceClient();
            BindingContext = new AttendeeBindingModel();
        }

        protected async void Register(object sender, EventArgs e)
        {
            var attendance = BindingContext as AttendeeBindingModel;

            // validate inputs
            var validationResult = await _attendanceValidator.ValidateAsync(attendance);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors
                    .Aggregate(string.Empty, (x, y) => x + " - " + y.ErrorMessage + Environment.NewLine);

                var result = await DisplayAlert("Warning",
                    $"There were some issues with the entry: {Environment.NewLine}{Environment.NewLine}{errors} {Environment.NewLine}Do you want to proceed?",
                    "Yes", "No");

                if (!result)
                {
                    return;
                }
            }

            // disable the button
            btnSave.IsVisible = false;
            prgSaving.IsVisible = true;
            await _attendanceService.AddAttendanceAsync(attendance);

            // alert the user
            await DisplayAlert("Success", "User successfully registered.", "Ok");

            // set the controls
            await ResetControlsAsync();
            prgSaving.IsVisible = false;
            btnSave.IsVisible = true;
        }

        protected async void EvaluateValidity(object sender, EventArgs e)
        {
            var attendance = BindingContext as Attendance;

            if (attendance == null)
            {
                return;
            }

            if (!attendance.CaredForSickPerson && !attendance.LiveWithCovidCaregivers &&
                !attendance.ReturnedInLastTenDays && attendance.HaveCovidSymptoms != "Yes")
            {
                return;
            }

            await DisplayAlert("Notice", "Sorry, this individual cannot be allowed into the service.", "Ok");
            await ResetControlsAsync();
        }

        private async Task ResetControlsAsync()
        {
            // reset binding context
            BindingContext = new Attendance();

            // drop downs
            cmbGender.SelectedIndex = -1;

            // scroll to top
            await scrollView.ScrollToAsync(0, 0, true);
        }
    }
}
