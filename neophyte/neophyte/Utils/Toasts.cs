using Acr.UserDialogs;
using Xamarin.Forms;

namespace neophyte.Utils
{
    internal static class Toasts
    {
        public static void DisplayError(string message)
        {
            UserDialogs.Instance.Toast(new ToastConfig(message)
            {
                BackgroundColor = Color.FromHex("#AA0B0F")
            });
        }
    }
}
