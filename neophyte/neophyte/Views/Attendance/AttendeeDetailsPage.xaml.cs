using neophyte.Models.View;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace neophyte.Views.Attendance
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AttendeeDetailsPage : ContentPage
    {
        public AttendeeDetailsPage(AttendeeViewModel attendee)
        {
            InitializeComponent();

            Title = "Attendee Details";
            SetValue(NavigationPage.BarBackgroundColorProperty, Color.FromHex("#52004C"));
            BindingContext = attendee;
        }
    }
}
