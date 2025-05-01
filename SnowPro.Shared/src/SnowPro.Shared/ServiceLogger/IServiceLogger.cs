using SnowPro.Shared.Entity;

namespace SnowPro.Shared.ServiceLogger;

public interface IServiceLogger : IObservable<ServiceLogMessage>
{
    void LogError(Exception exception, string message);
    void LogError(string message);
    void LogWarning(string message);
    void LogInformation(string message);
    void LogDebug(string message);
    void LogTrace(string message);
}