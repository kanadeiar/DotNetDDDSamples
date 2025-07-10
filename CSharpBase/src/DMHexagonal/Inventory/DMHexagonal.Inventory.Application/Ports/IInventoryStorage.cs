using DMHexagonal.Inventory.Core.InventoryAggregate;
using DMHexagonal.Inventory.Core.InventoryAggregate.Values;

namespace DMHexagonal.Inventory.Application.Ports;

public interface IInventoryStorage : ITransactionalStorage
{
    InventoryId NextIdentity();

    public IEnumerable<InventoryItem> All();

    InventoryItem Load(InventoryId id);

    void Save(InventoryItem aggregate);
}

public interface ITransactionalStorage
{
    public void BeginTransaction();
    public void Commit();
    public void Rollback();
}