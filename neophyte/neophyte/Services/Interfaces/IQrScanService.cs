using System.Threading.Tasks;

namespace neophyte.Services.Interfaces
{
    public interface IQrScanService
    {
        Task<string> ScanAsync();
    }
}