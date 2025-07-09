using DMHex.Inventory.Core.Base;
using DMHex.Inventory.Core.InventoryAggregate.Values;

namespace DMHex.Inventory.Core.InventoryAggregate.Events;

public record InventoryQuantityChanged(InventoryId Id, QuantityValue Quantity) : DomainEvent;
