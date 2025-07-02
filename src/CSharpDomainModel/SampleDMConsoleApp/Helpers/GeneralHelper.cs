using DomainModel.Inventory.Application.Develop;
using DomainModel.Inventory.Infra.Adapters;
using DomainModel.Inventory.Infra.Tools;
using DomainModel.Inventory.Application.InventoryFeature;

namespace SampleDMConsoleApp.Helpers;

public static class GeneralHelper
{
    public static InventoryScript CreateScript()
    {
        var storage = new InventoryStorage();
        var dispatcher = new DomainEventDispatcher();
        var inventoryScript = new InventoryScript(storage, dispatcher);
        DeveloperScript.RunExample(dispatcher);
        dispatcher.Run();
        return inventoryScript;
    }
}