using SnowPro.Shared.Entity;

namespace SnowPro.Shared.ServiceLogger;

internal class UnSubscriber : IDisposable
{
    private readonly HashSet<IObserver<ServiceLogMessage>> _subscribers;
    private readonly IObserver<ServiceLogMessage> _subscriber;
    
    public UnSubscriber(HashSet<IObserver<ServiceLogMessage>> subscribers,
        IObserver<ServiceLogMessage> subscriber)
    {
        _subscribers = subscribers;
        _subscriber = subscriber;
    }
    public void Dispose()
    {
        _subscribers.Remove(_subscriber);
    }
}