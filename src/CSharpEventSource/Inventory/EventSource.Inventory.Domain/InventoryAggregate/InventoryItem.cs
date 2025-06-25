using EventSource.Inventory.Contracts.Base;
using EventSource.Inventory.Domain.InventoryAggregate.Events;
using EventSource.Inventory.Domain.InventoryAggregate.Values;

namespace EventSource.Inventory.Domain.InventoryAggregate;

public class InventoryItem : AggregateRoot
{
    private InventoryId _id = default!;
    public override InventoryId Id => _id;

    public InventoryItem(InventoryId id, InventoryNameValue name, QuantityValue quantity)
    {
        ApplyChange(new InventoryCreated(id, name, quantity));
    }

    public InventoryItem() { }

    protected override void Mutate(DomainEvent @event) =>
        ((dynamic)this).when((dynamic)@event);

    private void when(InventoryCreated ev)
    {
        _id = ev.Id;
    }
}