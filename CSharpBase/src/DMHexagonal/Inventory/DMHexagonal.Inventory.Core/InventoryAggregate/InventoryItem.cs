using DMHexagonal.Inventory.Core.Base;
using DMHexagonal.Inventory.Core.Base.Abstractions;
using DMHexagonal.Inventory.Core.Entries;
using DMHexagonal.Inventory.Core.InventoryAggregate.Events;
using DMHexagonal.Inventory.Core.InventoryAggregate.Values;

namespace DMHexagonal.Inventory.Core.InventoryAggregate;

public class InventoryItem : AggregateRoot
{
    private readonly InventoryId _id;
    private InventoryNameValue _name;
    private QuantityValue _quantity;

    public override InventoryId Id => _id;

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

    private InventoryItem(InventoryId id, InventoryNameValue name, QuantityValue quantity)
    {
        _id = id;
        _name = name;
        _quantity = quantity;
    }

    public void Rename(InventoryNameValue newName)
    {
        _name = newName;
        ApplyChange(new InventoryRenamed(_id, newName));
    }

    public void Quantity(QuantityValue newQuantity)
    {
        _quantity = newQuantity;
        ApplyChange(new InventoryQuantityChanged(_id, newQuantity));
    }

    public InventoryEntry Entry() =>
        new()
        {
            Id = _id.Id,
            Name = _name.Name,
            Quantity = _quantity.Quantity,
        };

    public override string ToString() => $"{_name.Name} - {_quantity.Quantity} шт.";
}