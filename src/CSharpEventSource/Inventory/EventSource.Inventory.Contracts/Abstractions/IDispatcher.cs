namespace EventSource.Inventory.Contracts.Abstractions;

public interface IDispatcher
{
    void RegisterHandler<T>(Action<T> handler)
        where T : Base.DomainEvent;

    void Dispatch(IEnumerable<Base.DomainEvent> events);
}