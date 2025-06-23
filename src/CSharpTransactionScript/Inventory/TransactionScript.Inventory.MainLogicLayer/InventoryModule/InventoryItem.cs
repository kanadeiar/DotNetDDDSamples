using Kanadeiar.Common;
using TransactionScript.Inventory.DataAccessLayer.Contracts;

namespace TransactionScript.Inventory.MainLogicLayer.InventoryModule;

public class InventoryItem(int id, string name, int quantity = 0)
{
    private readonly string _name = name!.Require(name!.Length is >= 3 and <= 90, () =>
        throw new ApplicationException("Название должно быть длинной от 3 до 90 символов"));
    private readonly int _quantity = quantity.Require(quantity is >= 0 and <= 10000, () =>
        throw new ApplicationException("Количество должно быть от 0 до 10000 лет"));

    public static InventoryItem Restore(InventoryEntry entry)
    {
        return new InventoryItem(entry.Id, entry.Name, entry.Quantity);
    }

    public InventoryItem Rename(string newName) => new(id, newName, _quantity);

    public InventoryItem Quantity(int newQuantity) => new(id, _name, newQuantity);

    public InventoryEntry Entry()
    {
        return new InventoryEntry
        {
            Id = id,
            Name = _name,
            Quantity = _quantity
        };
    }

    public override string ToString() => $"{_name} - {_quantity} шт.";
}