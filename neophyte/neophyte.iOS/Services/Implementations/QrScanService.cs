using System;
using System.Threading.Tasks;
using Foundation;
using neophyte.iOS.Services.Implementations;
using neophyte.Services.Interfaces;
using Xamarin.Forms;
using ZXing.Mobile;

[assembly: Dependency(typeof(QrScanService))]
namespace neophyte.iOS.Services.Implementations
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

                var scanResult = await scanner.Scan(options, true);
                return scanResult?.Text;
            }
            catch (NSErrorException)
            {
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
