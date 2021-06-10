using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace neophyte.Components
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AttendanceRegistrationPopup : PopupPage
    {
        private readonly string _personId;
        private readonly Action _action;
        
        public AttendanceRegistrationPopup(string personId, Action action)
        {
            InitializeComponent();

            _personId = personId;
            _action = action;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            txtPersonId.Text = _personId;
        }

        protected async void RegisterAttendance(object sender, EventArgs e)
        {
            btnSubmit.IsVisible = false;
            prgSubmit.IsVisible = true;
            
        }
    }
}
