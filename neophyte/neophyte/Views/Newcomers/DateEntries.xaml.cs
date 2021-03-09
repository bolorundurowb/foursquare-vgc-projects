using System;
using System.Linq;
using System.Threading.Tasks;
using neophyte.DataAccess.Implementations;
using neophyte.Models.View;
using neophyte.Utils;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace neophyte.Views.Newcomers
{
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
            lblHeader.Text = _date.ToString("d MMMM \\'yy.");
            
            // load page data
            await LoadDateRecords();
            prgLoading.IsVisible = false;
            collectionDateEntries.IsVisible = true;
        }

        protected async void DeleteRecord(object sender, EventArgs e)
        {
            if (((SwipeItemView) sender).BindingContext is NewcomerViewModel newcomer)
            {
                await _newcomerClient.DeleteNewcomer(newcomer.Id);
                // refresh view
                collectionDateEntries.ItemsSource = _dateRecords.Where(x => x.Id != newcomer?.Id);

                // notify user
                Toasts.DisplaySuccess("Entry successfully removed.");
            }
            else
            {
                Toasts.DisplayError("An error occurred when deleting the entry.");
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

        private async Task LoadDateRecords()
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await DisplayAlert("Error", "There were issues retrieving data. Please check your internet connection",
                    "Close");
                return;
            }

            _dateRecords = await _newcomerClient.GetNewcomersForDate(_date);
            collectionDateEntries.ItemsSource = _dateRecords;
        }
    }
}
