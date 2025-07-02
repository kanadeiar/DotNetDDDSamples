using EventSource.Inventory.Contracts.Base;
using EventSource.Inventory.Domain.InventoryAggregate.Values;

namespace EventSource.Inventory.Domain.InventoryAggregate.Events;

public record InventoryCreated(InventoryId Id, InventoryNameValue Name, QuantityValue Quantity) : DomainEvent;