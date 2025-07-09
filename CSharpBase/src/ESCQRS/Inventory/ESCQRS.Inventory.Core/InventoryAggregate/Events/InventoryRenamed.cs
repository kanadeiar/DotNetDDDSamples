using ESCQRS.Inventory.Core.Base;
using ESCQRS.Inventory.Core.InventoryAggregate.Values;

namespace ESCQRS.Inventory.Core.InventoryAggregate.Events;

public record InventoryRenamed(InventoryId Id, InventoryNameValue Name) : DomainEvent;