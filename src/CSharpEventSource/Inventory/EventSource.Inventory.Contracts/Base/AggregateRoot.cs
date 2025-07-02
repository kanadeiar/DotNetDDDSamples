using EventSource.Inventory.Contracts.Abstractions;

namespace EventSource.Inventory.Contracts.Base;

public abstract class AggregateRoot
{
    private readonly List<DomainEvent> _changes = new();

    public abstract IId Id { get; }

    public int Version { get; private set; } = -1;

    public void Load(EventStream stream)
    {
        foreach (var @event in stream.Events) Mutate(@event);
        Version = stream.Version;
    }

    public ICollection<DomainEvent> Changes() => _changes;
    public void Reset() => _changes.Clear();

    protected void ApplyChange(DomainEvent @event)
    {
        Mutate(@event);
        _changes.Add(@event);
    }

    protected abstract void Mutate(DomainEvent @event);
}