using DMHexagonal.Inventory.Core.Base;
using DMHexagonal.Inventory.Core.Entries;
using DMHexagonal.Inventory.Core.InventoryAggregate.Events;
using DMHexagonal.Inventory.Core.InventoryAggregate.Values;

namespace DMHexagonal.Inventory.Core.InventoryAggregate;

public class InventoryItem(InventoryId id, InventoryNameValue name, QuantityValue quantity) : AggregateRoot
{
    private InventoryNameValue _name = name;
    private QuantityValue _quantity = quantity;

    public override InventoryId Id => id;

    public static InventoryItem Create(InventoryId id, InventoryNameValue name, QuantityValue quantity)
    {
        var result = new InventoryItem(id, name, quantity);
        result.ApplyChange(new InventoryCreated(id, name, quantity));
        return result;
    }

    public static InventoryItem Restore(InventoryEntry entry)
    {
        return new InventoryItem(new InventoryId(entry.Id), new InventoryNameValue(entry.Name), new QuantityValue(entry.Quantity));
    }

    public void Rename(InventoryNameValue newName)
    {
        _name = newName;
        ApplyChange(new InventoryRenamed(Id, newName));
    }

    public void Quantity(QuantityValue newQuantity)
    {
        _quantity = newQuantity;
        ApplyChange(new InventoryQuantityChanged(Id, newQuantity));
    }

    public InventoryEntry Entry() =>
        new()
        {
            Id = Id.Id,
            Name = _name.Name,
            Quantity = _quantity.Quantity,
        };

    public override string ToString() => $"{_name.Name} - {_quantity.Quantity} шт.";
}