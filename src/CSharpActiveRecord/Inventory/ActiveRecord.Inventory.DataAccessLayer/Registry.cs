using ActiveRecord.Inventory.DataAccessLayer.Data;

namespace ActiveRecord.Inventory.DataAccessLayer;

public class Registry
{
    private static Registry _inst = new();

    internal InventoryEntriesStorage storage = new();

    public static void InitFake(FakeRegistry fake) => _inst = fake;

    public static InventoryEntriesStorage Storage => _inst.storage;
}

public class FakeRegistry : Registry
{
    public InventoryEntriesStorage FakeStorage
    {
        get => storage;
        set => storage = value;
    }
}