using TS3L.Inventory.DataAccess.Data;
using TS3L.Inventory.Presentation.Scripts;

namespace TS3LayersConsoleApp.Helpers;

public static class GeneralApplicationHelper
{
    public static InventoryScript CreateScript()
    {
        var storage = new InventoryEntriesStorage();
        var script = new InventoryScript(storage);

        return script;
    }
}