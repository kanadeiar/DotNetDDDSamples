using DomainModel.Inventory.Contracts.Base;
using DomainModel.Inventory.Domain.InventoryAggregate.Values;

namespace DomainModel.Inventory.Domain.InventoryAggregate.Events;

public record InventoryRenamed(InventoryId Id, InventoryNameValue Name) : DomainEvent;