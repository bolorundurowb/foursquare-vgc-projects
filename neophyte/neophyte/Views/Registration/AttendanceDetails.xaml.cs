using neophyte.Firebase;
using neophyte.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace neophyte.Views.Registration
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AttendanceDetails : ContentPage
    {
        public AttendanceDetails(Attendance attendance)
        {
            InitializeComponent();

            Title = "Attendance Details";
            SetValue(NavigationPage.BarBackgroundColorProperty, Color.FromHex("#52004C"));
            BindingContext = attendance;
        }
    }
}
