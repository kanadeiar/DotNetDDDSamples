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

    public void Rename(InventoryNameValue newName)
    {
        ApplyChange(new InventoryRenamed(Id, newName));
    }

    public void Quantity(QuantityValue newQuantity)
    {
        ApplyChange(new InventoryQuantityChanged(Id, newQuantity));
    }

    protected override void Mutate(DomainEvent @event) =>
        ((dynamic)this).when((dynamic)@event);

    private void when(InventoryCreated ev)
    {
        _id = ev.Id;
    }

    private void when(InventoryRenamed ev) { }

    private void when(InventoryQuantityChanged ev) { }
}