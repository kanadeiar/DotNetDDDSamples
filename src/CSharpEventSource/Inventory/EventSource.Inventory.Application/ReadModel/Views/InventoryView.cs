using EventSource.Inventory.Application.Ports;
using EventSource.Inventory.Domain.InventoryAggregate.Events;
using EventSource.Inventory.Domain.ReadModel;

namespace EventSource.Inventory.Application.ReadModel.Views;

public class InventoryView(IReadModelStorage storage)
{
    public void Handle(InventoryCreated message)
    {
        var item = storage.All.Find(x => x.Id == message.Id);
        if (item is not { } found)
        {
            storage.All.Add(new InventoryProjection(message));
        }
    }

    public void Handle(InventoryRenamed message)
    {
        var item = storage.All.Find(f => f.Id == message.Id);
        if (item is not { } found)
        {
            throw new KeyNotFoundException(nameof(item));
        }
        var index = storage.All.IndexOf(found);
        var newest = found.Apply(message);
        storage.All[index] = newest;
    }

    public void Handle(InventoryQuantityChanged message)
    {
        var item = storage.All.Find(f => f.Id == message.Id);
        if (item is not { } found)
        {
            throw new KeyNotFoundException(nameof(item));
        }
        var index = storage.All.IndexOf(item);
        var newest = found.Apply(message);
        storage.All[index] = newest;
    }
}