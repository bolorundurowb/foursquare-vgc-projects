using neophyte.Models.View;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace neophyte.Views.Registration
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AttendeeDetails : ContentPage
    {
        public AttendeeDetails(AttendeeViewModel attendee)
        {
            InitializeComponent();

            Title = "Attendee Details";
            SetValue(NavigationPage.BarBackgroundColorProperty, Color.FromHex("#52004C"));
            BindingContext = attendee;
        }
    }
}
