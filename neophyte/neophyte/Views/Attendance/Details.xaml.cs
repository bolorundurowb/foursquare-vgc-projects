using System;
using System.Linq;
using System.Net.Http;
using MapsterMapper;
using neophyte.DataAccess.Implementations;
using neophyte.Models.Binding;
using neophyte.Models.View;
using neophyte.Utils;
using neophyte.Validators;
using Refit;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace neophyte.Views.Attendance
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AttendeeDetailsPage : ContentPage
    {
        private readonly AttendanceClient _attendanceClient;
        private readonly AttendanceValidator _attendanceValidator = new AttendanceValidator();
        private readonly IMapper _mapper = new Mapper();

        public AttendeeDetailsPage(AttendeeViewModel attendee, bool inEditMode = false)
        {
            InitializeComponent();

            Title = "Attendee Details";
            SetValue(NavigationPage.BarBackgroundColorProperty, Color.FromHex("#52004C"));

            // set dropdown values
            cmbGender.ItemsSource = Constants.Genders;

            // initialize stuff
            _attendanceClient = new AttendanceClient();
            SetAttendeeDisplayValue(attendee);

            if (inEditMode)
            {
                ShowEditControls();
            }
        }

        protected void EnableEditMode(object sender, EventArgs e)
        {
            ShowEditControls();
        }

        protected async void UpdateAttendee(object sender, EventArgs e)
        {
            var vm = BindingContext as AttendeeViewModel;
            var attendee = _mapper.Map<AttendeeUpdateBindingModel>(vm);

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

            btnUpdate.IsVisible = false;
            prgSaving.IsVisible = true;

            try
            {
                var response = await _attendanceClient.Update(vm.Id, attendee);

                // alert the user
                Toasts.DisplaySuccess("Attendee details updated successfully.");

                // set the display values
                SetAttendeeDisplayValue(response);
            }
            catch (ApiException ex)
            {
                await DisplayAlert("Error", ex.Content, "Okay");
            }
            catch (HttpRequestException)
            {
                await DisplayAlert("Error", "An error occurred.", "Okay");
            }

            btnUpdate.IsVisible = true;
            prgSaving.IsVisible = false;
            await scrollView.ScrollToAsync(0, 0, true);

            HideEditControls();
        }

        protected async void GoBack(object sender, EventArgs e)
        {
            await Navigation.PopAsync(true);
        }

        private void SetAttendeeDisplayValue(AttendeeViewModel attendee)
        {
            BindingContext = attendee;
        }

        private void ShowEditControls()
        {
            // hide the display entries
            lblAge.IsVisible = false;
            lblGender.IsVisible = false;
            lblEmailAddress.IsVisible = false;
            lblFullName.IsVisible = false;
            lblPhoneNumber.IsVisible = false;
            lblResidentialAddress.IsVisible = false;
            lblSeatNumber.IsVisible = false;
            lblCovidSymptoms.IsVisible = false;
            chkCaredForSick.IsVisible = false;
            chkLiveWithCaregivers.IsVisible = false;
            chkReturnedInTenDays.IsVisible = false;
            rdbCovidSymptoms.IsVisible = false;

            // show the inputs
            btnUpdate.IsVisible = true;
            txtAge.IsVisible = true;
            txtEmail.IsVisible = true;
            txtFullName.IsVisible = true;
            txtPhoneNumber.IsVisible = true;
            txtResidentialAddress.IsVisible = true;
            txtSeatNumber.IsVisible = true;
            cmbGender.IsVisible = true;
            
            // enable date picker
            dtpDate.IsEnabled = true;
        }

        private void HideEditControls()
        {
            // hide the display entries
            lblAge.IsVisible = true;
            lblGender.IsVisible = true;
            lblEmailAddress.IsVisible = true;
            lblFullName.IsVisible = true;
            lblPhoneNumber.IsVisible = true;
            lblResidentialAddress.IsVisible = true;
            lblSeatNumber.IsVisible = true;
            lblCovidSymptoms.IsVisible = true;
            chkCaredForSick.IsVisible = true;
            chkLiveWithCaregivers.IsVisible = true;
            chkReturnedInTenDays.IsVisible = true;
            rdbCovidSymptoms.IsVisible = true;

            // show the inputs
            btnUpdate.IsVisible = false;
            txtAge.IsVisible = false;
            txtEmail.IsVisible = false;
            txtFullName.IsVisible = false;
            txtPhoneNumber.IsVisible = false;
            txtResidentialAddress.IsVisible = false;
            txtSeatNumber.IsVisible = false;
            cmbGender.IsVisible = false;
            
            // disable date picker
            dtpDate.IsEnabled = false;
        }
    }
}
