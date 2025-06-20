using ActiveRecord.Inventory.DataAccessLayer;
using ActiveRecord.Inventory.DataAccessLayer.Contracts;
using Kanadeiar.Common;

namespace ActiveRecord.Inventory.MainLogicLayer.InventoryModule;

public class InventoryItem(int id, string? name, int quantity)
{
    public int RecordId => id;

    private readonly string _name = name!.Require(name!.Length is >= 3 and <= 90, () =>
        throw new ApplicationException("Название должно быть длинной от 3 до 90 символов"));
    private readonly int _quantity = quantity.Require(quantity is >= 0 and <= 10000, () =>
        throw new ApplicationException("Количество должно быть от 0 до 10000 лет"));

    public static Result<InventoryItem> Create(string? name, int quantity)
    {
        var id = Registry.Storage.NextIdentity();
        var item = new InventoryItem(id, name, quantity);
        return Result.Ok(item);
    }

    public static InventoryItem Create(InventoryEntry entry)
    {
        return new InventoryItem(entry.Id, entry.Name, entry.Quantity);
    }

    public InventoryItem Rename(string newName)
    {
        return new InventoryItem(RecordId, newName, _quantity);
    }

    public InventoryItem Quantity(int newQuantity)
    {
        return new InventoryItem(RecordId, _name, newQuantity);
    }

    public static Result<InventoryItem> Find(int id)
    {
        try
        {
            var entry = Registry.Storage.Load(id);
            if (entry is null) return Result.Fail<InventoryItem>($"Элемент с идентификатором {id} не найден");

            var result = new InventoryItem(entry.Id, 
                entry.Name, 
                entry.Quantity);

            return Result.Ok(result);
        }
        catch (Exception e)
        {
            return Result.Fail<InventoryItem>("Не удалось найти элемент. Ошибка: " + e);
        }
    }

    public Result Add()
    {
        try
        {
            var entity = new InventoryEntry
            {
                Id = RecordId,
                Name = name,
                Quantity = quantity,
            };

            Registry.Storage.Save(entity);

            return Result.Ok();
        }
        catch (Exception e)
        {
            return Result.Fail("Не удалось добавить элемент. Ошибка: " + e);
        }
    }

    public InventoryEntry Entry()
    {
        return new InventoryEntry
        {
            Id = id,
            Name = _name,
            Quantity = _quantity
        };
    }

    public override string ToString() => $"{_name} - {_quantity} шт.";
}