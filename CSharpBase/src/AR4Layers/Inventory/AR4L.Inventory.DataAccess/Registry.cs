using AR4L.Inventory.DataAccess.Data;

namespace AR4L.Inventory.DataAccess;

public class Registry
{
    private static Registry _inst = new();

    internal InventoryStorage storage = new();

    public static void InitFake(FakeRegistry fake) => _inst = fake;

    public static InventoryStorage Storage => _inst.storage;
}

public class FakeRegistry : Registry
{
    public InventoryStorage FakeStorage
    {
        get => storage;
        set => storage = value;
    }
}
