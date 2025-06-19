namespace TransactionScript.Inventory.DataAccessLayer.Contracts;

public class InventoryEntry
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int Quantity { get; set; }
}