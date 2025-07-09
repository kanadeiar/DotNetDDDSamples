using DMHex.Inventory.Core.Base;

namespace DMHex.Inventory.Core.Entries;

public class InventoryEntry : Entry
{
    public string Name { get; init; } = string.Empty;

    public int Quantity { get; init; }
}