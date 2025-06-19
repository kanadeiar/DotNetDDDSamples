using Kanadeiar.Common;
using TransactionScript.Inventory.DataAccessLayer.Data;
using TransactionScript.Inventory.MainLogicLayer.InventoryModule;

namespace TransactionScript.Inventory.PresentationLayer.Scripts;

public class InventoryScript(InventoryStorage storage)
{
    public void InitDemo()
    {
        AddItem("Колбаса");
        AddItem("Сыр");
        AddItem("Хлеб");
    }

    public Result<IEnumerable<string>> AllItems()
    {
        var items = storage.All().Select(InventoryItem.Create).Select(item => $"{item}");

        return Result.Ok(items);
    }

    public Result<int> AddItem(string name)
    {
        try
        {
            storage.BeginTransaction();

            var id = storage.NextIdentity();
            var item = new InventoryItem(id, name);
            var entry = item.Backup();

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
            var item = InventoryItem.Create(loaded);

            item = item.Rename(newName);

            var entry = item.Backup();
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
            var item = InventoryItem.Create(loaded);

            item = item.Quantity(newQuantity);

            var entry = item.Backup();
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