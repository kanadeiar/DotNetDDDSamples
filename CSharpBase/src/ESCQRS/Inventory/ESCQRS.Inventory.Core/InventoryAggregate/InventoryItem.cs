using ESCQRS.Inventory.Core.Base;
using ESCQRS.Inventory.Core.Base.Abstractions;
using ESCQRS.Inventory.Core.InventoryAggregate.Events;
using ESCQRS.Inventory.Core.InventoryAggregate.Values;

namespace ESCQRS.Inventory.Core.InventoryAggregate;

public class InventoryItem : AggregateRoot
{
    private InventoryId _id = null!;
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

    protected override void Mutate(IMessage @event) =>
        ((dynamic)this).when((dynamic)@event);

    private void when(InventoryCreated ev)
    {
        _id = ev.Id;
    }

    private void when(InventoryRenamed ev) { }

    private void when(InventoryQuantityChanged ev) { }
}