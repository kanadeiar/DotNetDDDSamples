using DMHexagonal.Inventory.Core.Base.Abstractions;

namespace DMHexagonal.Inventory.Core.Base;

public record DomainEvent : IMessage
{
    public DateTime OccurredOn { get; set; } = DateTime.Now;
}