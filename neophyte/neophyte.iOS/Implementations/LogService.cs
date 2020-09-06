using System.Xml;
using neophyte.Interfaces;
using neophyte.iOS.Implementations;
using NLog;
using NLog.Config;
using Xamarin.Forms;

[assembly: Dependency(typeof(LogService))]
namespace neophyte.iOS.Implementations
{
    public class LogService : ILogService
    {
        private readonly Logger _logger;

        public LogService()
        {
            var assembly = GetType().Assembly;
            const string location = "neophyte.iOS.NLog.config";
            var stream = assembly.GetManifestResourceStream(location);
            LogManager.Configuration = new XmlLoggingConfiguration(XmlReader.Create(stream), null);
            _logger = LogManager.GetCurrentClassLogger();
        }

        public void LogDebug(string message)
        {
            _logger.Info(message);
        }

        public void LogError(string message)
        {
            _logger.Error(message);
        }

        public void LogFatal(string message)
        {
            _logger.Fatal(message);
        }

        public void LogInfo(string message)
        {
            _logger.Info(message);
        }

        public void LogWarning(string message)
        {
            _logger.Warn(message);
        }
    }
}
