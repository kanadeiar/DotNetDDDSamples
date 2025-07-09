using EventSource.Inventory.Contracts.Abstractions;
using EventSource.Inventory.Contracts.Base;

namespace EventSource.Inventory.Infra.Data;

public class Storage<T>(IEventStore store) : IStorage<T>
    where T : AggregateRoot, new()
{
    public T Load(IId id)
    {
        var result = new T();
        var stream = store.LoadEventStream(id);
        result.Apply(stream);

        return result;
    }

    public void Save(T aggregate)
    {
        store.AppendToStream(aggregate.Id, aggregate.Changes(), aggregate.Version);
    }

    public void BeginTransaction() { }
    public void Commit() { }
    public void Rollback() { }
}