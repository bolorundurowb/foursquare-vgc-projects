using System;
using System.Linq;
using System.Threading.Tasks;
using neophyte.DataAccess.Implementations;
using neophyte.Models.View;
using neophyte.Services.Implementations;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace neophyte.Views.Newcomers
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewcomersByDatePage : ContentPage
    {
        private readonly NewcomerClient _newcomerClient;
        private readonly DateTime _date;
        private NewcomerViewModel[] _dateRecords;

        public NewcomersByDatePage(DateTime date)
        {
            InitializeComponent();

            _date = date;
            _newcomerClient = new NewcomerClient();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // set the header
            lblHeader.Text = _date.ToString("d MMM \\'yy.");

            // load page data
            await LoadDateRecords();
        }

        protected async void DeleteRecord(object sender, EventArgs e)
        {
            var shouldDelete = await DisplayAlert("Confirm Delete",
                "Are you sure you want to permanently delete this newcomer?", "Yes", "No");

            if (shouldDelete)
            {
                if (((SwipeItemView) sender).BindingContext is NewcomerViewModel newcomer)
                {
                    await _newcomerClient.DeleteNewcomer(newcomer.Id);

                    // refresh view
                    collectionDateEntries.ItemsSource = _dateRecords.Where(x => x.Id != newcomer.Id);

                    // notify user
                    ToastService.DisplaySuccess("Entry successfully removed.");
                }
                else
                {
                    ToastService.DisplayError("An error occurred when deleting the entry.");
                }
            }
            else
            {
                ToastService.DisplayInfo("Operation cancelled.");
            }
        }

        protected async void EditRecord(object sender, EventArgs e)
        {
            if (((SwipeItemView) sender).BindingContext is NewcomerViewModel newcomer)
            {
                await Navigation.PushAsync(new NewcomerDetailsPage(newcomer, true));
            }
        }

        protected async void ViewPersonRecord(object sender, EventArgs e)
        {
            if (collectionDateEntries.SelectedItem is NewcomerViewModel newcomer)
            {
                await Navigation.PushAsync(new NewcomerDetailsPage(newcomer));
            }
        }

        protected async void GoBack(object sender, EventArgs e)
        {
            await Navigation.PopAsync(true);
        }

        protected async void OnRefresh(object sender, EventArgs e)
        {
            await LoadDateRecords();
        }

        private async Task LoadDateRecords()
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await DisplayAlert("Error", "There were issues retrieving data. Please check your internet connection",
                    "Close");
                return;
            }

            rfsLoading.IsRefreshing = true;
            _dateRecords = await _newcomerClient.GetNewcomersForDate(_date);
            collectionDateEntries.ItemsSource = _dateRecords;
            rfsLoading.IsRefreshing = false;
        }
    }
}
