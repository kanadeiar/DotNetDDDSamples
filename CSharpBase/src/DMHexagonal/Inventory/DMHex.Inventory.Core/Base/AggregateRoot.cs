using DMHex.Inventory.Core.Base.Abstractions;

namespace DMHex.Inventory.Core.Base;

public abstract class AggregateRoot
{
    private readonly List<IMessage> _changes = new();

    public abstract IId Id { get; }

    protected void ApplyChange(IMessage @event)
    {
        _changes.Add(@event);
    }

    public IEnumerable<IMessage> Changes() => _changes;
    public void Reset() => _changes.Clear();
}