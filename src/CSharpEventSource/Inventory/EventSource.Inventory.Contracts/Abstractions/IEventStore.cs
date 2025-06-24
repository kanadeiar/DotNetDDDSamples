using EventSource.Inventory.Contracts.Base;
using System.Security.Principal;

namespace EventSource.Inventory.Contracts.Abstractions;

public interface IEventStore
{
    EventStream LoadEventStream(IId id);

    void AppendToStream(IId id, ICollection<DomainEvent> events, int expectedVersion);
}