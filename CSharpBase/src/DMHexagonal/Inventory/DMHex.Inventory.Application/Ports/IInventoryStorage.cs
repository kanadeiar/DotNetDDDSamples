using DMHex.Inventory.Core.InventoryAggregate;
using DMHex.Inventory.Core.InventoryAggregate.Values;
using Kanadeiar.Common.Functionals;

namespace DMHex.Inventory.Application.Ports;

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