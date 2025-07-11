using Kanadeiar.Common.Functionals;
using TS3L.Inventory.Core.InventoryModule;
using TS3L.Inventory.DataAccess.Data;

namespace TS3L.Inventory.Presentation.Scripts;

public class InventoryScript(InventoryEntriesStorage storage)
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

    public Result<IEnumerable<InventoryItem>> AllItems()
    {
        var items = storage.All().Select(InventoryItem.Restore);

        return Result.Ok(items);
    }

    public Result<int> AddItem(string name)
    {
        try
        {
            storage.BeginTransaction();
            var id = storage.NextIdentity();

            var item = new InventoryItem(id, name);

            storage.Save(item.Entry());
            storage.Commit();
            return Result.Ok(id);
        }
        catch (Exception e)
        {
            storage.Rollback();
            return Result.Fail<int>("Не удалось добавить новый элемент. Ошибка: " + e);
        }
    }

    public Result ChangeName(int id, string newName)
    {
        try
        {
            storage.BeginTransaction();
            var entry = storage.Load(id);
            if (entry is null) throw new ApplicationException("Элемент не найден");
            var item = InventoryItem.Restore(entry);

            var updated = item.Rename(newName);

            storage.Save(updated.Entry());
            storage.Commit();
            return Result.Ok();
        }
        catch (Exception e)
        {
            storage.Rollback();
            return Result.Fail("Не удалось изменить название элемента. Ошибка: " + e);
        }
    }

    public Result ChangeQuantity(int id, int newQuantity)
    {
        try
        {
            storage.BeginTransaction();
            var entry = storage.Load(id);
            if (entry is null) throw new ApplicationException("Элемент не найден");
            var item = InventoryItem.Restore(entry);

            var updated = item.Quantity(newQuantity);

            storage.Save(updated.Entry());
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

