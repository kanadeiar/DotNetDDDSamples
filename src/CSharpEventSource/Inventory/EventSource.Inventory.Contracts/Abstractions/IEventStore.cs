using EventSource.Inventory.Contracts.Base;

namespace EventSource.Inventory.Contracts.Abstractions;

public interface IEventStore
{
    EventStream LoadEventStream(IId id);

    void AppendToStream(IId id, ICollection<DomainEvent> events, int expectedVersion);
}