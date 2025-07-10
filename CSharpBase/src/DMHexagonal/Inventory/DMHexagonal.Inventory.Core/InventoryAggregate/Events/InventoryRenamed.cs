using DMHexagonal.Inventory.Core.Base;
using DMHexagonal.Inventory.Core.InventoryAggregate.Values;

namespace DMHexagonal.Inventory.Core.InventoryAggregate.Events;

public record InventoryRenamed(InventoryId Id, InventoryNameValue Name) : DomainEvent;