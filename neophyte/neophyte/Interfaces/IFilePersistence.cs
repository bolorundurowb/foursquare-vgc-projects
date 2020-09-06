using neophyte.Enums;

namespace neophyte.Interfaces
{
    public interface IFilePersistence
    {
        string SaveFile(string date, string csvContent, RecordType recordType);
    }
}
