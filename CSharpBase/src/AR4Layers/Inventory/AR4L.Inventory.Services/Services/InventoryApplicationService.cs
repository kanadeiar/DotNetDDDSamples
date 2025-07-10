using AR4L.Inventory.Core.InventoryModule;
using AR4L.Inventory.DataAccess;
using Kanadeiar.Common.Functionals;

namespace AR4L.Inventory.Services.Services;

public class InventoryApplicationService
{
    public Result<int> CreateNewInventoryItem(string name, int quantity)
    {
        try
        {
            Registry.Storage.BeginTransaction();

            var item = InventoryItem.Create(name, quantity);
            var result = item.Add();

            Registry.Storage.Commit();

            return result switch
            {
                IFail fail => Result.Fail<int>(fail.Error),
                not null => Result.Ok(item.Id),
                _ => throw new IndexOutOfRangeException(nameof(result)),
            };
        }
        catch (Exception e)
        {
            Registry.Storage.Rollback();
            return Result.Fail<int>("Не удалось добавить элемент. Ошибка: " + e);
        }
    }

    public Result<IEnumerable<string>> AllInventoryItems()
    {
        try
        {
            var items = Registry.Storage.All().Select(InventoryItem.Restore)
                .Select(item => $"{item}");

            return Result.Ok(items);
        }
        catch (Exception e)
        {
            return Result.Fail<IEnumerable<string>>("Не удалось получить все элементы. Ошибка: " + e);
        }
    }

    public Result ChangeNameOfInventoryItem(int id, string newName)
    {
        try
        {
            Registry.Storage.BeginTransaction();

            var entry = Registry.Storage.Load(id);
            if (entry == null) return Result.Fail("Не удалось найти элемент.");
            var item = InventoryItem.Restore(entry);

            var actual = item.Rename(newName);
            actual.Save();

            return Result.Ok();
        }
        catch (Exception e)
        {
            Registry.Storage.Rollback();
            return Result.Fail("Не удалось изменить название элемента. Ошибка: " + e);
        }
    }

    public Result ChangeQuantityOfInventoryItem(int id, int newQuantity)
    {
        try
        {
            Registry.Storage.BeginTransaction();

            var entry = Registry.Storage.Load(id);
            if (entry == null) return Result.Fail("Не удалось найти элемент.");
            var item = InventoryItem.Restore(entry);

            var actual = item.Quantity(newQuantity);
            actual.Save();

            return Result.Ok();
        }
        catch (Exception e)
        {
            Registry.Storage.Rollback();
            return Result.Fail("Не удалось изменить количество элемента. Ошибка: " + e);
        }
    }
}