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

    public Result AddItem(string name)
    {
        try
        {
            storage.BeginTransaction();

            var item = new InventoryItem(InventoryId.New, new InventoryNameValue(name), new QuantityValue(0));

            storage.Save(item);

            return Result.Ok();
        }
        catch (Exception e)
        {
            storage.Rollback();
            return Result.Fail("Не удалось добавить новый элемент. Ошибка: " + e);
        }
    }
}