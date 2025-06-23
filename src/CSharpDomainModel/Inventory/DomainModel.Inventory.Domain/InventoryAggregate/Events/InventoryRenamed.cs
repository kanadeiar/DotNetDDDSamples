using DomainModel.Inventory.Contracts.Base;
using DomainModel.Inventory.Domain.InventoryAggregate.Values;

namespace DomainModel.Inventory.Domain.InventoryAggregate.Events;

public record InventoryRenamed(InventoryId Id, string NewName) : DomainEvent
{
    public readonly InventoryNameValue Name = new(NewName);
}