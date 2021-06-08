using System.Threading.Tasks;
using neophyte.Droid.Services.Implementations;
using neophyte.Services.Interfaces;
using ZXing.Mobile;
using Xamarin.Forms;

[assembly: Dependency(typeof(QrScanService))]
namespace neophyte.Droid.Services.Implementations
{
    public class QrScanService : IQrScanService
    {
        public async Task<string> ScanAsync()
        {
            var optionsCustom = new MobileBarcodeScanningOptions();

            var scanner = new MobileBarcodeScanner
            {
                TopText = "Scan the QR Code",
                BottomText = "Scanning..."
            };

            var scanResult = await scanner.Scan(optionsCustom);
            return scanResult.Text;
        }
    }
}
