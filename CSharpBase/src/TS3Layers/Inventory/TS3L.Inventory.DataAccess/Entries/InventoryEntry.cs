namespace TS3L.Inventory.DataAccess.Entries;

public class InventoryEntry
{
    public int Id { get; init; }

    public string Name { get; init; } = string.Empty;

    public int Quantity { get; init; }
}