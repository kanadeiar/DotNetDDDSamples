using EventSource.Inventory.Application.Ports;
using EventSource.Inventory.Domain.InventoryAggregate.Events;
using EventSource.Inventory.Domain.ReadModel;

namespace EventSource.Inventory.Application.ReadModel.Views;

public class InventoryView(IReadModelStorage storage)
{
    public void Handle(InventoryCreated message)
    {
        storage.All.Add(new InventoryProjection(message));
    }
}