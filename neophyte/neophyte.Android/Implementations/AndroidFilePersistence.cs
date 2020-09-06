using System.IO;
using System.Text;
using Android.OS;
using neophyte.Droid.Implementations;
using neophyte.Enums;
using neophyte.Interfaces;
using Xamarin.Forms;

[assembly: Dependency(typeof(AndroidFilePersistence))]
namespace neophyte.Droid.Implementations
{
    public class AndroidFilePersistence : IFilePersistence
    {
        public string SaveFile(string date, string csvContent, RecordType recordType)
        {
            var sdCardPath = Environment.ExternalStorageDirectory.AbsolutePath;
            var neophyteFolder = Path.Combine(sdCardPath, "Neophyte");
            var recordFolder = Path.Combine(neophyteFolder, recordType.ToString());

            if (!Directory.Exists(recordFolder))
            {
                Directory.CreateDirectory(recordFolder);
            }

            var filePath = Path.Combine(recordFolder, $"{date}.csv");

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            File.WriteAllText(filePath, csvContent, Encoding.UTF8);
            return filePath;
        }
    }
}
