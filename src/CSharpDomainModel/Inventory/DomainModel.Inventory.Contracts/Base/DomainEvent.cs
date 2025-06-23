namespace DomainModel.Inventory.Contracts.Base;

public record DomainEvent
{
    public DateTime OccurredOn { get; set; } = DateTime.Now;
}