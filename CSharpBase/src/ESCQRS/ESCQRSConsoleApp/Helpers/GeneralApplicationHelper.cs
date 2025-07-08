using ESCQRS.Inventory.Application.InventoryFeature;
using ESCQRS.Inventory.Core.InventoryAggregate;
using ESCQRS.Inventory.Infra.Adapters;
using ESCQRS.Inventory.Infra.Tools;

namespace ESCQRSConsoleApp.Helpers;

public static class GeneralApplicationHelper
{
    public static InventoryApplicationService CreateService()
    {
        var dispatcher = GeneralApplicationHelper.dispatcher();
        var storage = GeneralApplicationHelper.storage(dispatcher);
        var master = ReadModelHelper.CreateMaster(dispatcher);

        var script = new InventoryApplicationService(storage, master);

        return script;
    }

    private static DomainEventDispatcher dispatcher()
    {
        var domainEventDispatcher = new DomainEventDispatcher();
        domainEventDispatcher.Run();
        return domainEventDispatcher;
    }

    private static Storage<InventoryItem> storage(DomainEventDispatcher dispatcher)
    {
        var store = new EventStore(dispatcher);
        var storage = new Storage<InventoryItem>(store);
        return storage;
    }
}