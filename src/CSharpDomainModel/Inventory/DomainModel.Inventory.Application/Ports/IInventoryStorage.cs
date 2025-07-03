using DomainModel.Inventory.Domain.InventoryAggregate;
using DomainModel.Inventory.Domain.InventoryAggregate.Values;
using Kanadeiar.Common.Functionals;

namespace DomainModel.Inventory.Application.Ports;

public interface IInventoryStorage
{
    InventoryId NextIdentity();

    public IEnumerable<InventoryItem> All();

    Result<InventoryItem> Load(InventoryId id);

    void Save(InventoryItem aggregate);

    public void BeginTransaction();
    public void Commit();
    public void Rollback();
}