using DomainModel.Inventory.Application.Ports;
using DomainModel.Inventory.Contracts.Entries;
using DomainModel.Inventory.Domain.InventoryAggregate;
using DomainModel.Inventory.Domain.InventoryAggregate.Values;
using Kanadeiar.Common;

namespace DomainModel.Inventory.Infra.Adapters;

public class InventoryStorage : IInventoryStorage
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

    public Result<InventoryItem> Load(InventoryId id)
    {
        var entry = _entries.GetValueOrDefault(id);
        if (entry == null) return Result.Fail<InventoryItem>("Не удалось найти сущность с идентификатором " + id.Id);

        var questionnaire = InventoryItem.Restore(entry);

        return Result.Ok(questionnaire);
    }

    public void Save(InventoryItem aggregate)
    {
        var entry = aggregate.Entry();

        _entries[aggregate.AggregateId] = entry;
    }

    public void BeginTransaction() { }
    public void Commit() { }
    public void Rollback() { }
}