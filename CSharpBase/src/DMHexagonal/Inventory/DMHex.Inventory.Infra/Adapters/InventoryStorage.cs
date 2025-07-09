using DMHex.Inventory.Application.Ports;
using DMHex.Inventory.Core.Base.Abstractions;
using DMHex.Inventory.Core.Entries;
using DMHex.Inventory.Core.InventoryAggregate;
using DMHex.Inventory.Core.InventoryAggregate.Values;
using Kanadeiar.Common.Functionals;

namespace DMHex.Inventory.Infra.Adapters;

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
        var events = aggregate.Changes();
        dispatcher.Dispatch(events);

        var entry = aggregate.Entry();

        _entries[aggregate.Id] = entry;
    }

    public void BeginTransaction() { }
    public void Commit() { }
    public void Rollback() { }
}