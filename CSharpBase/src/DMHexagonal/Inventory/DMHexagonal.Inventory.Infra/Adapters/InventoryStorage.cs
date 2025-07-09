using DMHexagonal.Inventory.Application.Ports;
using DMHexagonal.Inventory.Core.Base.Abstractions;
using DMHexagonal.Inventory.Core.Entries;
using DMHexagonal.Inventory.Core.InventoryAggregate;
using DMHexagonal.Inventory.Core.InventoryAggregate.Values;

namespace DMHexagonal.Inventory.Infra.Adapters;

public class InventoryStorage(IDispatcher dispatcher) : IInventoryStorage
{
    private Dictionary<InventoryId, InventoryEntry> _entries = new();
    private int _lastId;

    public InventoryId NextIdentity() => new(++_lastId);

    public IEnumerable<InventoryItem> All()
    {
        foreach (var entry in _entries.Values)
        {
            yield return InventoryItem.Restore(entry);
        }
    }

    public InventoryItem Load(InventoryId id)
    {
        var entry = _entries.GetValueOrDefault(id);
        if (entry == null) throw new ApplicationException("Не удалось найти сущность с идентификатором " + id.Id);

        var questionnaire = InventoryItem.Restore(entry);

        return questionnaire;
    }

    public void Save(InventoryItem aggregate)
    {
        var entry = aggregate.Entry();

        _entries[aggregate.Id] = entry;

        var events = aggregate.Changes();
        dispatcher.Dispatch(events);
    }

    public void BeginTransaction() { }
    public void Commit() { }
    public void Rollback() { }
}