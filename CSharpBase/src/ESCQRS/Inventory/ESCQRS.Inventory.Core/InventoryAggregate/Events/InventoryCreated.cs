using ESCQRS.Inventory.Core.Base;
using ESCQRS.Inventory.Core.InventoryAggregate.Values;

namespace ESCQRS.Inventory.Core.InventoryAggregate.Events;

public record InventoryCreated(InventoryId Id, InventoryNameValue Name, QuantityValue Quantity) : DomainEvent;