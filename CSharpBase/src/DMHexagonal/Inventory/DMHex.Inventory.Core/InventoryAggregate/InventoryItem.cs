using DMHex.Inventory.Core.Base;
using DMHex.Inventory.Core.Entries;
using DMHex.Inventory.Core.InventoryAggregate.Events;
using DMHex.Inventory.Core.InventoryAggregate.Values;

namespace DMHex.Inventory.Core.InventoryAggregate;

public class InventoryItem(InventoryId id, InventoryNameValue name, QuantityValue quantity) : AggregateRoot
{
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

    public InventoryItem Rename(InventoryNameValue newName)
    {
        var result = new InventoryItem(id, newName, quantity);
        result.ApplyChange(new InventoryRenamed(id, newName));
        return result;
    }

    public InventoryItem ChangeQuantity(QuantityValue newQuantity)
    {
        var result = new InventoryItem(id, name, newQuantity);
        result.ApplyChange(new InventoryQuantityChanged(id, newQuantity));
        return result;
    }

    public InventoryEntry Entry()
    {
        return new InventoryEntry
        {
            Id = id.Id,
            Name = name.Name,
            Quantity = quantity.Quantity,
        };
    }

    public override string ToString() => $"{name.Name} - {quantity.Quantity} шт.";
}