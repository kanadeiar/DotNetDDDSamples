using EventSource.Inventory.Contracts.Base;

namespace EventSource.Inventory.Contracts.Abstractions;

public interface IStorage<out T>
    where T : AggregateRoot, new()
{
    T Load(IId id);

    void Save(AggregateRoot aggregate);

    public void BeginTransaction();
    public void Commit();
    public void Rollback();
}