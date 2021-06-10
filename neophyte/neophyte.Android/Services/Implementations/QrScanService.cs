using System.Threading.Tasks;
using Java.Lang;
using neophyte.Droid.Services.Implementations;
using neophyte.Services.Interfaces;
using ZXing.Mobile;
using Xamarin.Forms;
using Application = Android.App.Application;

[assembly: Dependency(typeof(QrScanService))]
namespace neophyte.Droid.Services.Implementations
{
    public class QrScanService : IQrScanService
    {
        public async Task<string> ScanAsync()
        {
            try
            {
                var options = new MobileBarcodeScanningOptions();
                var scanner = new MobileBarcodeScanner
                {
                    TopText = "Scan the QR Code",
                    BottomText = "Scanning..."
                };

                var scanResult = await scanner.Scan(Application.Context, options);
                return scanResult?.Text;
            }
            catch (Exception)
            {
                return null;
            }
            catch (System.Exception)
            {
                return null;
            }
        }
    }
}
