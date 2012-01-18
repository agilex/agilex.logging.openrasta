using System;
using OpenRasta;
using OpenRasta.Diagnostics;
using log4net;

namespace agilex.logging.openrasta
{
    public class Log4NetLogger : ILogger
    {
        readonly ILog _log;

        public Log4NetLogger(ILog log)
        {
            _log = log;
        }

        #region ILogger Members

        public IDisposable Operation(object source, string name)
        {
            _log.DebugFormat("Entering {0}: {1}", source.GetType().Name, name);
            return new OperationCookie(_log, source);
        }

        public void WriteDebug(string message, params object[] format)
        {
            _log.DebugFormat(message, format);
        }

        public void WriteWarning(string message, params object[] format)
        {
            _log.WarnFormat(message, format);
        }

        public void WriteError(string message, params object[] format)
        {
            _log.ErrorFormat(message, format);
        }

        public void WriteInfo(string message, params object[] format)
        {
            _log.InfoFormat(message, format);
        }

        public void WriteException(Exception e)
        {
            _log.Error("Exception", e);
        }

        #endregion

        #region Nested type: OperationCookie

        class OperationCookie : IDisposable
        {
            readonly ILog _log;
            readonly object _source;

            public OperationCookie(ILog log, object source)
            {
                _log = log;
                _source = source;
            }

            #region IDisposable Members

            public void Dispose()
            {
                _log.DebugFormat("Exiting {0}".With(_source.GetType().Name));
            }

            #endregion
        }

        #endregion
    }

    public class Log4NetLogger<T> : Log4NetLogger, ILogger<T> where T : ILogSource
    {
        public Log4NetLogger() : base(LogManager.GetLogger(LogSource<T>.Category))
        {
        }
    }
}