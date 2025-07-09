using DMHex.Inventory.Application.Ports;
using DMHex.Inventory.Core.InventoryAggregate;
using DMHex.Inventory.Core.InventoryAggregate.Values;
using Kanadeiar.Common.Functionals;

namespace DMHex.Inventory.Application.InventoryFeature;

public class InventoryApplicationService(IInventoryStorage storage)
{
    public Result InitDemo()
    {
        try
        {
            AddItem(new InventoryNameValue("Колбаса")).Throw(fail => new ApplicationException(fail.Error));
            AddItem(new InventoryNameValue("Сыр")).Throw(fail => new ApplicationException(fail.Error));
            AddItem(new InventoryNameValue("Хлеб")).Throw(fail => new ApplicationException(fail.Error));

            return Result.Ok();
        }
        catch (Exception e)
        {
            return Result.Fail("Не удалось инициализировать демонстрационные данные. Ошибка: " + e);
        }
    }

    public Result<IEnumerable<string>> AllItems()
    {
        var items = storage.All().Select(item => $"{item}");

        return Result.Ok(items);
    }

    public Result<InventoryId> AddItem(InventoryNameValue name)
    {
        try
        {
            storage.BeginTransaction();
            var id = storage.NextIdentity();

            var item = new InventoryItem(id, name, new QuantityValue(0));

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

    public Result ChangeName(InventoryId id, InventoryNameValue newName)
    {
        try
        {
            storage.BeginTransaction();
            var item = storage.Load(id);

            item.Rename(newName);

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

    public Result ChangeQuantity(InventoryId id, QuantityValue newQuantity)
    {
        try
        {
            storage.BeginTransaction();
            var item = storage.Load(id);

            item.ChangeQuantity(newQuantity);

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