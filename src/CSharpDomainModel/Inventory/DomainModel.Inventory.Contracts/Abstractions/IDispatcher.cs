namespace DomainModel.Inventory.Contracts.Abstractions;

public interface IDispatcher
{
    void RegisterHandler<T>(Action<T> handler)
        where T : Base.DomainEvent;

    public void Dispatch(IEnumerable<Base.DomainEvent> events);
}