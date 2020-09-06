using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;
using TabbedPage = Xamarin.Forms.TabbedPage;

namespace neophyte.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RootPage : TabbedPage
    {
        public RootPage()
        {
            InitializeComponent();
            On<Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);

            if (Device.RuntimePlatform == Device.iOS)
            {
                BarTextColor = Color.Black;
            }
        }
    }
}
