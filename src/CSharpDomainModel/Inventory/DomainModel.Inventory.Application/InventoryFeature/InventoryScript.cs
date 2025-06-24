using DomainModel.Inventory.Application.Ports;
using DomainModel.Inventory.Contracts.Abstractions;
using DomainModel.Inventory.Domain.InventoryAggregate;
using DomainModel.Inventory.Domain.InventoryAggregate.Values;
using Kanadeiar.Common;

namespace DomainModel.Inventory.Application.InventoryFeature;

public class InventoryScript(IInventoryStorage storage, IDispatcher dispatcher)
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
        var items = storage.All().Select(item => $"{item}");

        return Result.Ok(items);
    }

    public Result<InventoryId> AddItem(string name)
    {
        try
        {
            storage.BeginTransaction();
            var id = storage.NextIdentity();
            
            var item = InventoryItem.Create(id, new InventoryNameValue(name), new QuantityValue(0));

            var events = item.TakeEvents();
            dispatcher.Dispatch(events);
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

    public Result ChangeName(int id, string newName)
    {
        try
        {
            storage.BeginTransaction();
            var item = storage.Load(new InventoryId(id))
                .TryGetValue(fail => throw new ApplicationException(fail.Error));

            item = item.Rename(newName);

            var events = item.TakeEvents();
            dispatcher.Dispatch(events);
            storage.Save(item);
            storage.Commit();
            return Result.Ok();
        }
        catch (Exception e)
        {
            storage.Rollback();
            return Result.Fail("Не удалось добавить новый элемент. Ошибка: " + e);
        }
    }

    public Result ChangeQuantity(int id, int newQuantity)
    {
        try
        {
            storage.BeginTransaction();
            var item = storage.Load(new InventoryId(id))
                .TryGetValue(fail => throw new ApplicationException(fail.Error));

            item = item.Quantity(newQuantity);

            var events = item.TakeEvents();
            dispatcher.Dispatch(events);
            storage.Save(item);
            storage.Commit();
            return Result.Ok();
        }
        catch (Exception e)
        {
            storage.Rollback();
            return Result.Fail("Не удалось добавить новый элемент. Ошибка: " + e);
        }
    }
}