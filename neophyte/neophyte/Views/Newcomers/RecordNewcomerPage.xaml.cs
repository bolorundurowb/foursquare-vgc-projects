using System;
using System.Linq;
using System.Threading.Tasks;
using neophyte.Firebase;
using neophyte.Models;
using neophyte.Validators;
using Xamarin.Forms;

namespace neophyte.Views.Newcomers
{
    public partial class RecordNewcomerPage : ContentPage
    {
        private readonly RecordService _recordService;
        private readonly RecordValidator _recordValidator = new RecordValidator();

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
            _recordService = new RecordService();
            BindingContext = new Record();
        }

        protected async void AddNewRecord(object sender, EventArgs e)
        {
            var record = (Record) BindingContext;
            record.BirthDay = $"{cmbMonths.SelectedItem} {cmbDays.SelectedItem}";

            // validate inputs
            var validationResult = await _recordValidator.ValidateAsync(record);
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
            await _recordService.CreateRecordAsync(record);

            // alert the user
            await DisplayAlert("Success", "Record successfully added.", "Ok");

            // set the controls
            await ResetControlsAsync();
            prgSaving.IsVisible = false;
            btnSave.IsVisible = true;
        }

        private async Task ResetControlsAsync()
        {
            // reset binding context
            BindingContext = new Record();

            // drop downs
            cmbDays.SelectedIndex = -1;
            cmbMonths.SelectedIndex = -1;

            // scroll to top
            await scrollView.ScrollToAsync(0, 0, true);
        }
    }
}
