using ESCQRS.Inventory.Core.InventoryAggregate.Events;
using ESCQRS.Inventory.Core.InventoryAggregate.Values;

namespace ESCQRS.Inventory.Core.ReadModel;

public record InventoryProjection(InventoryId Id, InventoryNameValue Name, QuantityValue Quantity)
{
    public InventoryProjection(InventoryCreated ev) : this(ev.Id, ev.Name, ev.Quantity) { }

    public InventoryProjection Apply(InventoryRenamed ev) =>
        this with { Name = ev.Name };

    public InventoryProjection Apply(InventoryQuantityChanged ev) =>
        this with { Quantity = ev.Quantity };

    public override string ToString() => $"{Name} - {Quantity} шт.";
}