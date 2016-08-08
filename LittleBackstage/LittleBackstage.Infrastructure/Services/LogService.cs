
using System.Reflection;
using log4net;

namespace LittleBackstage.Infrastructure.Services
{
    public interface ILogService : IDependency
    {
        void Debug(string msg);

        void Error(string msg);
    }

    public class LogService : ILogService
    {
        private readonly ILog _log = LogManager.GetLogger("AppLog.Logging");

        public void Debug(string msg)
        {
            _log.Debug(msg);
        }

        public void Error(string msg)
        {
            _log.Error(msg);
        }
    }
}
