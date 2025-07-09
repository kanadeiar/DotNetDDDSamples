using DMHex.Inventory.Core.Base;
using DMHex.Inventory.Core.InventoryAggregate.Values;

namespace DMHex.Inventory.Core.InventoryAggregate.Events;

public record InventoryCreated(InventoryId Id, InventoryNameValue Name, QuantityValue Quantity) : DomainEvent;
