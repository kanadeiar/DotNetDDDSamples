namespace DMHexagonal.Inventory.Core.Entries;

public class InventoryEntry
{
    public int Id { get; init; }

    public string Name { get; init; } = string.Empty;

    public int Quantity { get; init; }
}