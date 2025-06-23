namespace DomainModel.Inventory.Contracts.Entries;

public class InventoryEntry : Base.Entry
{
    public string Name { get; init; } = string.Empty;

    public int Quantity { get; init; }
}