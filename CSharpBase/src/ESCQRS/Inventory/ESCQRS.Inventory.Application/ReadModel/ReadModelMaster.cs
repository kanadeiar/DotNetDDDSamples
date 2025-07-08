using ESCQRS.Inventory.Application.ReadModel.Handlers;
using ESCQRS.Inventory.Application.ReadModel.Ports;
using ESCQRS.Inventory.Core.Abstractions.Base.Abstractions;
using ESCQRS.Inventory.Core.InventoryAggregate.Events;
using ESCQRS.Inventory.Core.ReadModel;

namespace ESCQRS.Inventory.Application.ReadModel;

public class ReadModelMaster(IReadModelStorage storage)
{
    public IEnumerable<InventoryProjection> Inventories => storage.Inventories;

    public void Init(IDispatcher dispatcher)
    {
        var view = new InventoryHandler(storage);
        dispatcher.RegisterHandler<InventoryCreated>(view.Handle);
        dispatcher.RegisterHandler<InventoryRenamed>(view.Handle);
        dispatcher.RegisterHandler<InventoryQuantityChanged>(view.Handle);
    }
}