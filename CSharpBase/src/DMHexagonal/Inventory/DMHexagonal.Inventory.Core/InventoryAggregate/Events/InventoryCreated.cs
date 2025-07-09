using DMHexagonal.Inventory.Core.Base;
using DMHexagonal.Inventory.Core.InventoryAggregate.Values;

namespace DMHexagonal.Inventory.Core.InventoryAggregate.Events;

public record InventoryCreated(InventoryId Id, InventoryNameValue Name, QuantityValue Quantity) : DomainEvent;