using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using neophyte.DataAccess.Implementations;
using neophyte.Models.Binding;
using neophyte.Utils;
using neophyte.Validators;
using Refit;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace neophyte.Views.Newcomers
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecordNewcomerPage : ContentPage
    {
        private readonly NewcomerClient _newcomerClient;
        private readonly NewcomerValidator _newcomerValidator = new NewcomerValidator();

        public RecordNewcomerPage()
        {
            InitializeComponent();

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
                Toasts.DisplaySuccess("Newcomer successfully recorded.");

                // set the controls
                await ResetControlsAsync();
            }
            catch (ApiException ex)
            {
                await DisplayAlert("Error", ex.Content, "Okay");
            }
            catch (HttpRequestException)
            {
                await DisplayAlert("Error", "An error occurred.", "Okay");
            }

            // enable buttons
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
