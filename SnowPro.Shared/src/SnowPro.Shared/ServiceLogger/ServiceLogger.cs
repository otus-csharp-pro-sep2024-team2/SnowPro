using SnowPro.Shared.Entity;

namespace SnowPro.Shared.ServiceLogger;

public class ServiceLogger :IServiceLogger
{
    private readonly HashSet<IObserver<ServiceLogMessage>> _subscribers = [];
    
    public IDisposable Subscribe(IObserver<ServiceLogMessage> observer)
    {
        if (!_subscribers.Contains(observer))
        {
            _subscribers.Add(observer);
        }

        return new UnSubscriber(_subscribers, observer);    }
    private void Log(string message, ServiceLogType logType)
    {
        foreach (var subscriber in _subscribers) {
            subscriber?.OnNext(new ServiceLogMessage(message, logType));
        };
    }

    public void LogError(Exception exception, string message)
    {
        Log($"{message}: {exception.Message} ", ServiceLogType.Error);
    }

    public void LogError(string message)
    {
        Log(message, ServiceLogType.Error);
    }

    public void LogWarning(string message)
    {
        Log(message, ServiceLogType.Warning);
    }

    public void LogInformation(string message)
    {
        Log(message, ServiceLogType.Information);
    }

    public void LogDebug(string message)
    {
        Log(message, ServiceLogType.Debug);
    }

    public void LogTrace(string message)
    {
        Log(message, ServiceLogType.Trace);
    }
}