namespace neophyte.Logging.Interfaces
{
    public interface ILogService
    {
        void LogDebug(string message);
        
        void LogError(string message);
        
        void LogFatal(string message);
        
        void LogInfo(string message);
        
        void LogWarning(string message);
    }
}
