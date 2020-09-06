using System;
using System.IO;
using System.Text;
using neophyte.Enums;
using neophyte.Interfaces;
using neophyte.iOS.Implementations;
using Xamarin.Forms;

[assembly: Dependency(typeof(IosFilePersistence))]
namespace neophyte.iOS.Implementations
{
    public class IosFilePersistence : IFilePersistence
    {
        public string SaveFile(string date, string csvContent, RecordType recordType)
        {
            var documentsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var neophyteFolder = Path.Combine(documentsFolder, "Neophyte");
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
