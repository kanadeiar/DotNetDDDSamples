namespace EventSource.Inventory.Contracts.Base;

public record EventStream(ICollection<DomainEvent> Events, int Version = -1);
