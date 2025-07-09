using DMHexagonal.Inventory.Core.Base;
using DMHexagonal.Inventory.Core.InventoryAggregate.Values;

namespace DMHexagonal.Inventory.Core.InventoryAggregate.Events;

public record InventoryQuantityChanged(InventoryId Id, QuantityValue Quantity) : DomainEvent;