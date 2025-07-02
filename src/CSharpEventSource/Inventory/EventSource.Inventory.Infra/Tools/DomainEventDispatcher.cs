using EventSource.Inventory.Contracts.Abstractions;
using EventSource.Inventory.Contracts.Base;

namespace EventSource.Inventory.Infra.Tools;

public class DomainEventDispatcher : IDispatcher
{
    private readonly Lock _lock = new();
    private readonly Dictionary<Type, List<Action<DomainEvent>>> _routes = new();
    private readonly List<DomainEvent> _events = new();

    public void RegisterHandler<T>(Action<T> handler)
        where T : DomainEvent
    {
        lock (_lock)
        {
            if (!_routes.TryGetValue(typeof(T), out var handlers))
            {
                handlers = new List<Action<DomainEvent>>();
                _routes.Add(typeof(T), handlers);
            }

            handlers.Add(message => handler((T)message));
        }
    }

    public void Dispatch(IEnumerable<DomainEvent> events)
    {
        lock (_lock)
        {
            _events.AddRange(events);
        }
    }

    public void Run()
    {
        Task.Run(async () =>
        {
            while (true)
            {
                lockedHandle();

                await Task.Delay(10);
            }
        });
    }

    private void lockedHandle()
    {
        lock (_lock)
        {
            try
            {
                handleAllEvents();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }

    private void handleAllEvents()
    {
        if (!_events.Any()) return;

        foreach (var each in _events)
        {
            if (_routes.TryGetValue(each.GetType(), out var handlers))
            {
                Array.ForEach(handlers.ToArray(), action => action.Invoke(each));
            }
        }

        _events.Clear();
    }
}