using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using neophyte.DataAccess.Implementations;
using neophyte.Firebase;
using neophyte.Models;
using neophyte.Models.Binding;
using neophyte.Validators;
using Refit;
using Xamarin.Forms;

namespace neophyte.Views.Newcomers
{
    public partial class RecordNewcomerPage : ContentPage
    {
        private readonly NewcomerClient _newcomerClient;
        private readonly NewcomerValidator _newcomerValidator = new NewcomerValidator();

        public RecordNewcomerPage()
        {
            InitializeComponent();

            Title = "Add Record";
            SetValue(NavigationPage.BarBackgroundColorProperty, Color.FromHex("#52004C"));

            // set drop down values
            cmbMonths.ItemsSource = Constants.Months;
            cmbDays.ItemsSource = Enumerable.Range(1, 31).ToList();
            cmbAgeGroup.ItemsSource = Constants.AgeGroups;
            cmbGender.ItemsSource = Constants.Genders;

            // initialize stuff
            _newcomerClient = new NewcomerClient();
            BindingContext = new NewcomerBindingModel();
        }

        protected async void Record(object sender, EventArgs e)
        {
            var newcomer = BindingContext as NewcomerBindingModel;

            // validate inputs
            var validationResult = await _newcomerValidator.ValidateAsync(newcomer);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors
                    .Aggregate(string.Empty, (x, y) => x + " - " + y.ErrorMessage + Environment.NewLine);

                var result = await DisplayAlert("Warning",
                    $"There were some issues with the entry: {Environment.NewLine}{Environment.NewLine}{errors} {Environment.NewLine}Do you want to proceed?",
                    "Yes", "No");

                if (!result)
                {
                    return;
                }
            }

            // disable the button
            btnSave.IsVisible = false;
            prgSaving.IsVisible = true;

            try
            {
                newcomer.BirthDay = $"{cmbMonths.SelectedItem} {cmbDays.SelectedItem}";
                await _newcomerClient.Register(newcomer);
                // alert the user
                await DisplayAlert("Success", "Attendee successfully registered.", "Okay");
                // set the controls
                await ResetControlsAsync();
            }
            catch (ApiException ex)
            {
                await DisplayAlert("Error", ex.Content, "Okay");
            }
            catch (HttpRequestException ex)
            {
                await DisplayAlert("Error", "An error occurred.", "Okay");
            }

            prgSaving.IsVisible = false;
            btnSave.IsVisible = true;
        }

        private async Task ResetControlsAsync()
        {
            // reset binding context
            BindingContext = new NewcomerBindingModel();

            // drop downs
            cmbDays.SelectedIndex = -1;
            cmbMonths.SelectedIndex = -1;

            // scroll to top
            await scrollView.ScrollToAsync(0, 0, true);
        }
    }
}