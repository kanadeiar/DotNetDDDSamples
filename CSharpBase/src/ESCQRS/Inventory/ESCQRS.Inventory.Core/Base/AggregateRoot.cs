using ESCQRS.Inventory.Core.Base.Abstractions;

namespace ESCQRS.Inventory.Core.Base;

public abstract class AggregateRoot
{
    private readonly List<IMessage> _changes = new();

    public abstract IId Id { get; }

    public int Version { get; private set; } = -1;

    public void Apply(EventStream stream)
    {
        foreach (var @event in stream.Events) Mutate(@event);
        Version = stream.Version;
    }

    public ICollection<IMessage> Changes() => _changes;
    public void Reset() => _changes.Clear();

    protected void ApplyChange(IMessage @event)
    {
        Mutate(@event);
        _changes.Add(@event);
    }

    protected abstract void Mutate(IMessage @event);
}