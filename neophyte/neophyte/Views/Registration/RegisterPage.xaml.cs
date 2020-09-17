using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using neophyte.DataAccess.Implementations;
using neophyte.Firebase;
using neophyte.Models;
using neophyte.Models.Binding;
using neophyte.Validators;
using Refit;
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
            var attendee = BindingContext as AttendeeBindingModel;

            // validate inputs
            var validationResult = await _attendanceValidator.ValidateAsync(attendee);
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

            try
            {
                await _attendanceClient.Register(attendee);
                // alert the user
                await DisplayAlert("Success", "User successfully registered.", "Okay");
                // set the controls
                await ResetControlsAsync();
            }
            catch (ApiException ex)
            {
                await DisplayAlert("Error", ex.Content, "Okay");
            }
            catch (HttpRequestException ex)
            {
                await DisplayAlert("Error", "An error occurred.", "Okay");
            }

            prgSaving.IsVisible = false;
            btnSave.IsVisible = true;
        }

        protected async void EvaluateValidity(object sender, EventArgs e)
        {
            if (!(BindingContext is AttendeeBindingModel attendance))
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
            BindingContext = new AttendeeBindingModel();

            // drop downs
            cmbGender.SelectedIndex = -1;

            // scroll to top
            await scrollView.ScrollToAsync(0, 0, true);
        }
    }
}
