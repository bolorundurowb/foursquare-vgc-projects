using System;
using System.Linq;
using System.Net.Http;
using MapsterMapper;
using neophyte.DataAccess.Implementations;
using neophyte.Models.Binding;
using neophyte.Models.View;
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

        public AttendeeDetailsPage(AttendeeViewModel attendee)
        {
            InitializeComponent();

            Title = "Attendee Details";
            SetValue(NavigationPage.BarBackgroundColorProperty, Color.FromHex("#52004C"));

            // set dropdown values
            cmbGender.ItemsSource = Constants.Genders;

            // initialize stuff
            _attendanceClient = new AttendanceClient();
            SetAttendeeDisplayValue(attendee);
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
                await DisplayAlert("Success", "Attendee details updated successfully.", "Okay");
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
            chkCaredForSick.IsEnabled = true;
            chkLiveWithCaregivers.IsEnabled = true;
            chkReturnedInTenDays.IsEnabled = true;
            rdbCovidSymptoms.IsEnabled = true;
            
            // show the inputs
            btnUpdate.IsVisible = true;
            txtAge.IsVisible = true;
            txtEmail.IsVisible = true;
            txtFullName.IsVisible = true;
            txtPhoneNumber.IsVisible = true;
            txtResidentialAddress.IsVisible = true;
            txtSeatNumber.IsVisible = true;
            cmbGender.IsVisible = true;

            // disable the menu option
            menuEdit.IsEnabled = false;
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
            chkCaredForSick.IsEnabled = false;
            chkLiveWithCaregivers.IsEnabled = false;
            chkReturnedInTenDays.IsEnabled = false;
            rdbCovidSymptoms.IsEnabled = false;
            
            // show the inputs
            btnUpdate.IsVisible = false;
            txtAge.IsVisible = false;
            txtEmail.IsVisible = false;
            txtFullName.IsVisible = false;
            txtPhoneNumber.IsVisible = false;
            txtResidentialAddress.IsVisible = false;
            txtSeatNumber.IsVisible = false;
            cmbGender.IsVisible = false;

            // disable the menu option
            menuEdit.IsEnabled = true;
        }
    }
}