using EventSource.Inventory.Application.Ports;
using EventSource.Inventory.Application.ReadModel.Views;
using EventSource.Inventory.Contracts.Abstractions;
using EventSource.Inventory.Domain.InventoryAggregate.Events;
using EventSource.Inventory.Domain.ReadModel;

namespace EventSource.Inventory.Application.ReadModel;

public class ReadModelMaster(IReadModelStorage storage)
{
    public IEnumerable<InventoryProjection> Inventories => storage.All;

    public void Init(IDispatcher dispatcher)
    {
        var view = new InventoryView(storage);
        dispatcher.RegisterHandler<InventoryCreated>(view.Handle);
        dispatcher.RegisterHandler<InventoryRenamed>(view.Handle);
        dispatcher.RegisterHandler<InventoryQuantityChanged>(view.Handle);
    }
}