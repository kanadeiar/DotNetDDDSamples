using DomainModel.Inventory.Contracts.Entries;
using DomainModel.Inventory.Domain.InventoryAggregate.Values;
using DomainModel.Inventory.Domain.InventoryAggregate.Events;

namespace DomainModel.Inventory.Domain.InventoryAggregate;

public class InventoryItem(InventoryId id, InventoryNameValue name, QuantityValue quantity) : Base.Aggregate
{
    public InventoryId AggregateId => id;

    public static InventoryItem Create(InventoryId id, InventoryNameValue name, QuantityValue quantity)
    {
        var result = new InventoryItem(id, name, quantity);
        result.AddEvent(new InventoryCreated(id));
        return result;
    }

    public static InventoryItem Restore(InventoryEntry entry)
    {
        return new InventoryItem(new InventoryId(entry.Id), new InventoryNameValue(entry.Name), new QuantityValue(entry.Quantity));
    }
    
    public InventoryItem Rename(string newName)
    {
        var result = new InventoryItem(id, new InventoryNameValue(newName), quantity);
        result.AddEvent(new InventoryRenamed(id, newName));
        return result;
    }
    
    public InventoryItem Quantity(int newQuantity)
    {
        var result = new InventoryItem(id, name, new QuantityValue(newQuantity));
        result.AddEvent(new InventoryQuantityChanged(id, newQuantity));
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