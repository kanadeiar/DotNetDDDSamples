using ESCQRS.Inventory.Application.ReadModel.Ports;
using ESCQRS.Inventory.Core.InventoryAggregate.Events;
using ESCQRS.Inventory.Core.InventoryAggregate.Values;
using ESCQRS.Inventory.Core.ReadModel;

namespace ESCQRS.Inventory.Application.ReadModel.Handlers;

public class InventoryHandler(IReadModelStorage storage)
{
    public void Handle(InventoryCreated message)
    {
        var item = storage.Inventories.Find(x => x.Id == message.Id);
        if (item is not { } found)
        {
            storage.Inventories.Add(new InventoryProjection(message));
        }
    }

    public void Handle(InventoryRenamed message) => 
        apply(message.Id, p => p.Apply(message));

    public void Handle(InventoryQuantityChanged message) =>
        apply(message.Id, p => p.Apply(message));

    private void apply(InventoryId id, Func<InventoryProjection, InventoryProjection> action)
    {
        var item = storage.Inventories.Find(f => f.Id == id);
        if (item is not { } found)
        {
            throw new KeyNotFoundException(nameof(item));
        }
        var index = storage.Inventories.IndexOf(found);
        var newest = action.Invoke(found);
        storage.Inventories[index] = newest;
    }
}