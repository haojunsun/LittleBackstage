
using System.Reflection;
using log4net;

namespace LittleBackstage.Infrastructure.Services
{
    public interface ILogService : IDependency
    {
        void Debug(string msg);
    }

    public class LogService : ILogService
    {
        private readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public void Debug(string msg)
        {
            _log.Debug(msg);
        }
    }
}
