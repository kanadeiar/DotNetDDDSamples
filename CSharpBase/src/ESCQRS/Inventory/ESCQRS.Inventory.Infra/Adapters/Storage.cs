using ESCQRS.Inventory.Core.Base;
using ESCQRS.Inventory.Core.Base.Abstractions;

namespace ESCQRS.Inventory.Infra.Adapters;

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