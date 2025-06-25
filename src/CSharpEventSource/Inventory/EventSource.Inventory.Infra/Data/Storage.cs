using EventSource.Inventory.Contracts.Abstractions;
using EventSource.Inventory.Contracts.Base;

namespace EventSource.Inventory.Infra.Data;

public class Storage<T>(IEventStore storage) : IStorage<T>
    where T : AggregateRoot, new()
{
    public T GetById(IId id)
    {
        var result = new T();
        var stream = storage.LoadEventStream(id);
        result.Load(stream);
        return result;
    }

    public void Save(AggregateRoot aggregate)
    {
        storage.AppendToStream(aggregate.Id, aggregate.Changes(), aggregate.Version);
    }

    public void BeginTransaction() { }
    public void Commit() { }
    public void Rollback() { }
}