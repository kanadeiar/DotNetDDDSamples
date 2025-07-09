using DMHexagonal.Inventory.Application.InventoryFeature;
using DMHexagonal.Inventory.Application.Ports;
using DMHexagonal.Inventory.Infra.Adapters;
using DMHexagonal.Inventory.Infra.Tools;
using DMHexagonalConsoleApp.Scripts;

namespace DMHexagonalConsoleApp.Helpers;

public static class GeneralApplicationHelper
{
    public static InventoryApplicationService CreateService()
    {
        var dispatcher = GeneralApplicationHelper.dispatcher();
        var storage = GeneralApplicationHelper.storage(dispatcher);
        DeveloperScript.RunExampleSubscribe(dispatcher);
        var script = new InventoryApplicationService(storage);

        return script;
    }

    private static DomainEventDispatcher dispatcher()
    {
        var domainEventDispatcher = new DomainEventDispatcher();
        domainEventDispatcher.Run();
        return domainEventDispatcher;
    }

    private static IInventoryStorage storage(DomainEventDispatcher dispatcher)
    {
        return new InventoryStorage(dispatcher);
    }
}