using SnowPro.Shared.Entity;

namespace SnowPro.Shared.ServiceLogger;

public class LogWrapper
{
    public static readonly object _locker = new ();
    public LogWrapper(Serilog.ILogger logger, IServiceLogger serviceLogger) 
    {
        serviceLogger.Subscribe(new Observer(logger));
    }

    private sealed class Observer : IObserver<ServiceLogMessage>
    {
        private readonly Serilog.ILogger _logger;

        public Observer(Serilog.ILogger logger)
        {
            _logger = logger;
        }

        public void OnCompleted()
        {
        }

        public void OnError(Exception error)
        {
        }

        public void OnNext(ServiceLogMessage value)
        {
            lock (_locker)
            {
                switch (value.LogType)
                {
                    case ServiceLogType.Information:
                        _logger.Information(value.Message);
                        break;
                    case ServiceLogType.Warning:
                        _logger.Warning(value.Message);
                        break;
                    case ServiceLogType.Error:
                        _logger.Error(value.Message);
                        break;
                }
            }
        }
    }
}