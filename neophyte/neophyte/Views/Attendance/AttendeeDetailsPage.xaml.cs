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

        private void SetAttendeeDisplayValue(AttendeeViewModel attendee)
        {
            BindingContext = attendee;
        }
    }
}