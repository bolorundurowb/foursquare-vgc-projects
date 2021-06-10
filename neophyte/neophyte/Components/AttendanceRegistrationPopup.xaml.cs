using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using neophyte.DataAccess.Implementations;
using neophyte.Models.Binding;
using neophyte.Services.Implementations;
using Refit;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace neophyte.Components
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AttendanceRegistrationPopup : PopupPage
    {
        private readonly AttendanceV2Client _attendanceClient;
        private readonly string _personId;
        
        public AttendanceRegistrationPopup(string personId)
        {
            InitializeComponent();
            
            _personId = personId;
            _attendanceClient = new AttendanceV2Client();
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

            try
            {
                int.TryParse(txtSeatNumber.Text, out var seatNumber);
                var attendee = new AttendeeV2BindingModel
                {
                    SeatNumber = seatNumber,
                    PersonId = _personId
                };

                await _attendanceClient.Register(attendee);

                // alert the user
                ToastService.DisplaySuccess("Attendee successfully registered.");

                await Navigation.PopModalAsync();
            }
            catch (ApiException ex)
            {
                await DisplayAlert("Error", ex.Content, "Okay");
            }
            catch (HttpRequestException)
            {
                await DisplayAlert("Error", "An error occurred.", "Okay");
            }
            finally
            {
                prgSubmit.IsVisible = false;
                btnSubmit.IsVisible = true;
            }
        }
    }
}
