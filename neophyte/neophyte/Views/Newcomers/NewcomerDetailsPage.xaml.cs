using System;
using System.Linq;
using System.Net.Http;
using MapsterMapper;
using neophyte.DataAccess.Implementations;
using neophyte.Models.Binding;
using neophyte.Models.View;
using neophyte.Validators;
using Refit;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace neophyte.Views.Newcomers
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewcomerDetailsPage : ContentPage
    {
        private readonly NewcomerClient _newcomerClient;
        private readonly NewcomerValidator _newcomerValidator = new NewcomerValidator();
        private readonly IMapper _mapper = new Mapper();

        public NewcomerDetailsPage(NewcomerViewModel newcomer)
        {
            InitializeComponent();

            Title = "Newcomer Details";
            SetValue(NavigationPage.BarBackgroundColorProperty, Color.FromHex("#52004C"));

            // set drop down values
            cmbMonths.ItemsSource = Constants.Months;
            cmbDays.ItemsSource = Enumerable.Range(1, 31).ToList();
            cmbAgeGroup.ItemsSource = Constants.AgeGroups;
            cmbGender.ItemsSource = Constants.Genders;

            // initialize stuff
            _newcomerClient = new NewcomerClient();
            SetNewcomerDisplayValues(newcomer);
        }

        protected void EnableEditMode(object sender, EventArgs e)
        {
            ShowEditControls();
        }

        protected async void UpdateRecord(object sender, EventArgs e)
        {
            var vm = BindingContext as NewcomerViewModel;
            var newcomer = _mapper.Map<NewcomerUpdateBindingModel>(vm);

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

            // mutate controle
            menuEdit.IsEnabled = false;
            btnUpdate.IsVisible = false;
            prgSaving.IsVisible = true;

            try
            {
                newcomer.BirthDay = $"{cmbMonths.SelectedItem} {cmbDays.SelectedItem}";
                var response = await _newcomerClient.Update(vm.Id, newcomer);
                // alert the user
                await DisplayAlert("Success", "Newcomer details updated successfully.", "Okay");
                // set the display values
                SetNewcomerDisplayValues(response);
            }
            catch (ApiException ex)
            {
                await DisplayAlert("Error", ex.Content, "Okay");
            }
            catch (HttpRequestException)
            {
                await DisplayAlert("Error", "An error occurred.", "Okay");
            }

            // mutate controls
            menuEdit.IsEnabled = true;
            btnUpdate.IsVisible = true;
            prgSaving.IsVisible = false;
            await scrollView.ScrollToAsync(0, 0, true);

            HideEditControls();
        }

        private void SetNewcomerDisplayValues(NewcomerViewModel newcomer)
        {
            // set the binding model
            BindingContext = newcomer;

            var birthdayParts = newcomer.BirthDay.Split(new[]
            {
                " "
            }, StringSplitOptions.RemoveEmptyEntries);
            if (birthdayParts.Length > 0)
            {
                cmbMonths.SelectedIndex = Constants.Months.IndexOf(birthdayParts[0]);
            }

            if (birthdayParts.Length > 1)
            {
                int.TryParse(birthdayParts[1], out var index);
                cmbDays.SelectedIndex = index - 1;
            }
        }

        private void ShowEditControls()
        {
            // hide the display
            lblBirthday.IsVisible = false;
            lblFullName.IsVisible = false;
            lblHomeAddress.IsVisible = false;
            lblPhone.IsVisible = false;
            lblEmailAddress.IsVisible = false;
            lblGender.IsVisible = false;
            lblHowYouFoundUs.IsVisible = false;
            lblAgeGroup.IsVisible = false;
            lblCommentsPrayers.IsVisible = false;

            // show the inputs
            btnUpdate.IsVisible = true;
            txtFullName.IsVisible = true;
            txtHomeAddress.IsVisible = true;
            txtPhone.IsVisible = true;
            txtEmail.IsVisible = true;
            txtHowYouFoundUs.IsVisible = true;
            txtPrayersComments.IsVisible = true;
            txtRemarks.IsVisible = true;

            stkBirthday.IsVisible = true;
            cmbAgeGroup.IsVisible = true;
            cmbGender.IsVisible = true;

            // disable the menu option
            menuEdit.IsEnabled = false;
        }

        private void HideEditControls()
        {
            // hide the display
            lblBirthday.IsVisible = true;
            lblFullName.IsVisible = true;
            lblHomeAddress.IsVisible = true;
            lblPhone.IsVisible = true;
            lblEmailAddress.IsVisible = true;
            lblHowYouFoundUs.IsVisible = true;
            lblAgeGroup.IsVisible = true;
            lblGender.IsVisible = true;
            lblCommentsPrayers.IsVisible = true;
            lblRemarks.IsVisible = true;

            // show the inputs
            btnUpdate.IsVisible = false;
            txtFullName.IsVisible = false;
            txtHomeAddress.IsVisible = false;
            txtPhone.IsVisible = false;
            txtEmail.IsVisible = false;
            txtHowYouFoundUs.IsVisible = false;
            txtPrayersComments.IsVisible = false;
            txtRemarks.IsVisible = false;

            stkBirthday.IsVisible = false;
            cmbAgeGroup.IsVisible = false;
            cmbGender.IsVisible = false;
        }
    }
}