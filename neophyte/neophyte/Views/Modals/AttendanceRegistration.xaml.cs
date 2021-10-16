using System;
using System.Net;
using System.Net.Http;
using neophyte.DataAccess.Implementations;
using neophyte.Models.Binding;
using neophyte.Models.View;
using neophyte.Services.Implementations;
using Refit;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms.Xaml;

namespace neophyte.Views.Modals
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AttendanceRegistration : Popup
    {
        private readonly AttendanceV2Client _attendanceClient;
        private readonly string _personId;

        public AttendanceRegistration(string personId)
        {
            InitializeComponent();

            _personId = personId;
            _attendanceClient = new AttendanceV2Client();
            lblPersonId.Text = _personId;
        }

        protected async void RegisterAttendance(object sender, EventArgs e)
        {
            stkButtons.IsVisible = false;
            prgSubmit.IsVisible = true;

            try
            {
                var attendee = new AttendeeV2BindingModel
                {
                    SeatAssigned = txtSeatNumber.Text,
                    PersonId = _personId
                };

                await _attendanceClient.Register(attendee);

                // alert the user
                ToastService.DisplaySuccess("Attendee successfully registered.");

                Dismiss(null);
            }
            catch (ApiException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                var error = await ex.GetContentAsAsync<ErrorViewModel>();
                ToastService.DisplayError(error?.Message);
            }
            catch (ApiException ex) when (ex.StatusCode == HttpStatusCode.Conflict)
            {
                var error = await ex.GetContentAsAsync<ErrorViewModel>();
                ToastService.DisplayError(error?.Message);
                Dismiss(null);
            }
            catch (ApiException ex)
            {
                ToastService.DisplayError(ex.Content ?? "An error occured.");
            }
            catch (HttpRequestException)
            {
                ToastService.DisplayError("An error occurred.");
            }
            finally
            {
                prgSubmit.IsVisible = false;
                stkButtons.IsVisible = true;
            }
        }

        protected void ClosePopup(object sender, EventArgs e)
        {
            Dismiss(null);
        }

        protected override object GetLightDismissResult()
        {
            return null;
        }
    }
}
