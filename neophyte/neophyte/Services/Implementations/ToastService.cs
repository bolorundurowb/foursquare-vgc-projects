using Acr.UserDialogs;
using Xamarin.Forms;

namespace neophyte.Services.Implementations
{
    internal static class ToastService
    {
        public static void DisplayError(string message)
        {
            Display(message, "#AA0B0F");
        }
        
        public static void DisplaySuccess(string message)
        {
            Display(message, "#4D9947");
        }
        
        public static void DisplayInfo(string message)
        {
            Display(message, "#49ABE8");
        }

        private static void Display(string message, string colorHex)
        {
            UserDialogs.Instance.Toast(new ToastConfig(message)
            {
                BackgroundColor = Color.FromHex(colorHex)
            });
        }
    }
}
