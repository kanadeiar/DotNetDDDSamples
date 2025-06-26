using EventSource.Inventory.Application.ReadModel;
using EventSource.Inventory.Contracts.Abstractions;
using EventSource.Inventory.Domain.InventoryAggregate;
using EventSource.Inventory.Domain.InventoryAggregate.Values;
using Kanadeiar.Common;

namespace EventSource.Inventory.Application.InventoryFeature;

public class InventoryScript(IStorage<InventoryItem> storage, Master master)
{
    public Result InitDemo()
    {
        try
        {
            AddItem("Колбаса").Throw(fail => new ApplicationException(fail.Error));
            AddItem("Сыр").Throw(fail => new ApplicationException(fail.Error));
            AddItem("Хлеб").Throw(fail => new ApplicationException(fail.Error));

            return Result.Ok();
        }
        catch (Exception e)
        {
            return Result.Fail("Не удалось инициализировать демонстрационные данные. Ошибка: " + e);
        }
    }

    public Result<IEnumerable<string>> AllItems()
    {
        var items = master.Inventories.Select(item => $"{item}");

        return Result.Ok(items);
    }

    public Result<InventoryId> AddItem(string name)
    {
        try
        {
            storage.BeginTransaction();
            var id = InventoryId.New;
            
            var item = new InventoryItem(id, new InventoryNameValue(name), new QuantityValue(0));

            storage.Save(item);
            storage.Commit();
            return Result.Ok(id);
        }
        catch (Exception e)
        {
            storage.Rollback();
            return Result.Fail<InventoryId>("Не удалось добавить новый элемент. Ошибка: " + e);
        }
    }

    public Result ChangeName(Guid id, string newName)
    {
        try
        {
            storage.BeginTransaction();
            var item = storage.GetById(new InventoryId(id));

            item.Rename(new InventoryNameValue(newName));

            storage.Save(item);
            storage.Commit();
            return Result.Ok();
        }
        catch (Exception e)
        {
            storage.Rollback();
            return Result.Fail("Не удалось изменить название элемента. Ошибка: " + e);
        }
    }

    public Result ChangeQuantity(Guid id, int newQuantity)
    {
        try
        {
            storage.BeginTransaction();
            var item = storage.GetById(new InventoryId(id));

            item.Quantity(new QuantityValue(newQuantity));

            storage.Save(item);
            storage.Commit();
            return Result.Ok();
        }
        catch (Exception e)
        {
            storage.Rollback();
            return Result.Fail("Не удалось изменить количество элемента. Ошибка: " + e);
        }
    }
}