using Kanadeiar.Common.Functionals;
using TransactionScript.Inventory.DataAccessLayer.Data;
using TransactionScript.Inventory.MainLogicLayer.InventoryModule;

namespace TransactionScript.Inventory.PresentationLayer.Scripts;

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

    public Result<IEnumerable<string>> AllItems()
    {
        var items = storage.All().Select(InventoryItem.Restore).Select(item => $"{item}");

        return Result.Ok(items);
    }

    public Result<int> AddItem(string name)
    {
        try
        {
            storage.BeginTransaction();

            var id = storage.NextIdentity();
            var item = new InventoryItem(id, name);
            var entry = item.Entry();

            storage.Save(entry);
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
            var loaded = storage.Load(id);
            var item = InventoryItem.Restore(loaded);

            item = item.Rename(newName);

            var entry = item.Entry();
            storage.Save(entry);
            storage.Commit();
            return Result.Ok();
        }
        catch (Exception e)
        {
            storage.Rollback();
            return Result.Fail("Не удалось изменить имя элемента. Ошибка: " + e);
        }
    }

    public Result ChangeQuantity(int id, int newQuantity)
    {
        try
        {
            storage.BeginTransaction();
            var loaded = storage.Load(id);
            var item = InventoryItem.Restore(loaded);

            item = item.Quantity(newQuantity);

            var entry = item.Entry();
            storage.Save(entry);
            storage.Commit();
            return Result.Ok();
        }
        catch (Exception e)
        {
            storage.Rollback();
            return Result.Fail("Не удалось изменить количество элементов. Ошибка: " + e);
        }
    }
}