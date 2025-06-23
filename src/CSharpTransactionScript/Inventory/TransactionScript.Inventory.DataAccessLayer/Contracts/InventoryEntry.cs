namespace TransactionScript.Inventory.DataAccessLayer.Contracts;

public class InventoryEntry
{
    public int Id { get; init; }

    public string Name { get; init; } = string.Empty;

    public int Quantity { get; init; }
}