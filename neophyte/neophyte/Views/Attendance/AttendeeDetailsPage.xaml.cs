using System;
using MapsterMapper;
using neophyte.DataAccess.Implementations;
using neophyte.Models.View;
using neophyte.Validators;
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
            // cmbGender.ItemsSource = Constants.Genders;

            // initialize stuff
            _attendanceClient = new AttendanceClient();
            SetAttendeeDisplayValue(attendee);
        }

        protected void EnableEditMode(object sender, EventArgs e)
        {
            ShowEditControls();
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
        }
    }
}