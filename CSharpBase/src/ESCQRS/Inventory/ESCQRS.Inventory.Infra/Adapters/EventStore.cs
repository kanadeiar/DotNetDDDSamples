using ESCQRS.Inventory.Core.Abstractions.Base;
using ESCQRS.Inventory.Core.Abstractions.Base.Abstractions;

namespace ESCQRS.Inventory.Infra.Adapters;

public class EventStore(IDispatcher dispatcher) : IEventStore
{
    private readonly Dictionary<IId, List<EventDescriptor>> _desctriptors = new();

    public EventStream LoadEventStream(IId id)
    {
        if (!_desctriptors.TryGetValue(id, out var descriptors))
        {
            throw new AggregateNotFoundException();
        }

        return new EventStream(descriptors.Select(descriptor => descriptor.Data).ToList(),
            descriptors.Last().Version);
    }

    public void AppendToStream(IId id, ICollection<IMessage> events, int expectedVersion)
    {
        if (!_desctriptors.TryGetValue(id, out var descriptors))
        {
            descriptors = new List<EventDescriptor>();
            _desctriptors.Add(id, descriptors);
        }
        else if (descriptors.Last().Version != expectedVersion && expectedVersion != -1)
        {
            throw new ConcurrencyException();
        }
        var version = expectedVersion;

        foreach (var @event in events)
        {
            version++;
            descriptors.Add(new EventDescriptor(@event, version));
        }

        dispatcher.Dispatch(events);
    }

    private record EventDescriptor(IMessage Data, int Version);

    public class AggregateNotFoundException : Exception;

    public class ConcurrencyException : Exception;
}