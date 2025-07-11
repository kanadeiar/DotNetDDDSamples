using TS3L.Inventory.DataAccess.Entries;

namespace TS3L.Inventory.DataAccess.Data;

public class InventoryEntriesStorage
{
    private Dictionary<int, InventoryEntry> _entries = new();
    private int _lastId;

    public int NextIdentity() => ++_lastId;

    public IEnumerable<InventoryEntry> All() => _entries.Values;

    public InventoryEntry? Load(int id) => _entries.GetValueOrDefault(id);

    public void Save(InventoryEntry entry) => _entries[entry.Id] = entry;

    public void BeginTransaction() { }
    public void Commit() { }
    public void Rollback() { }
}