using System;
using System.Net;
using System.Net.Http;
using neophyte.DataAccess.Implementations;
using neophyte.Models.Binding;
using neophyte.Models.View;
using neophyte.Services.Implementations;
using Refit;
using Xamarin.Forms.Xaml;

namespace neophyte.Views.Modals
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
            stkButtons.IsVisible = false;
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
            catch (ApiException ex) when (ex.StatusCode == HttpStatusCode.NotFound ||
                                          ex.StatusCode == HttpStatusCode.Conflict)
            {
                var error = await ex.GetContentAsAsync<ErrorViewModel>();
                ToastService.DisplayError(error.Message);
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

        protected async void ClosePopup(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}
