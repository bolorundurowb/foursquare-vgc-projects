using neophyte.Models.View;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace neophyte.Views.Registration
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AttendanceDetails : ContentPage
    {
        public AttendanceDetails(AttendeeViewModel attendee)
        {
            InitializeComponent();

            Title = "Attendance Details";
            SetValue(NavigationPage.BarBackgroundColorProperty, Color.FromHex("#52004C"));
            BindingContext = attendee;
        }
    }
}
