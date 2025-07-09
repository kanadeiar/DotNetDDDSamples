using DMHex.Inventory.Core.Base.Abstractions;

namespace DMHex.Inventory.Core.Base;

public record DomainEvent : IMessage
{
    public DateTime OccurredOn { get; set; } = DateTime.Now;
}