using EventSource.Inventory.Application.InventoryFeature;
using EventSource.Inventory.Domain.InventoryAggregate;
using EventSource.Inventory.Infra.Data;
using EventSource.Inventory.Infra.Tools;

namespace SampleESConsoleApp.Helpers;

public static class GeneralHelper
{
    public static InventoryApplicationService CreateService()
    {
        var dispatcher = GeneralHelper.dispatcher();
        var storage = GeneralHelper.storage(dispatcher);
        var master = ReadModelHelper.CreateReadModel(dispatcher);

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