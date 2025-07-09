namespace ESCQRS.Inventory.Core.Base.Abstractions;

public interface IStorage<T> : ITransactionalStorage
    where T : AggregateRoot, new()
{
    T Load(IId id);

    void Save(T aggregate);
}

public interface ITransactionalStorage
{
    public void BeginTransaction();
    public void Commit();
    public void Rollback();
}