using EventSource.Inventory.Domain.InventoryAggregate.Events;
using EventSource.Inventory.Domain.InventoryAggregate.Values;

namespace EventSource.Inventory.Domain.ReadModel;

public record InventoryProjection(InventoryId Id, InventoryNameValue Name, QuantityValue Quantity)
{
    public InventoryProjection(InventoryCreated ev) : this(ev.Id, ev.Name, ev.Quantity) { }

    public override string ToString() => $"{Name} - {Quantity} шт.";
}