﻿using ESCQRS.Inventory.Application.ReadModel;
using ESCQRS.Inventory.Core.Base.Abstractions;
using ESCQRS.Inventory.Core.InventoryAggregate;
using ESCQRS.Inventory.Core.InventoryAggregate.Values;
using ESCQRS.Inventory.Core.ReadModel;
using Kanadeiar.Common.Functionals;

namespace ESCQRS.Inventory.Application.InventoryFeature;

public class InventoryApplicationService(IStorage<InventoryItem> storage, ReadModelMaster master)
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

    public Result<IEnumerable<InventoryProjection>> AllItems()
    {
        var items = master.Inventories;

        return Result.Ok(items);
    }

    public Result<InventoryId> AddItem(InventoryNameValue name)
    {
        try
        {
            storage.BeginTransaction();
            var id = InventoryId.New;

            var item = InventoryItem.Create(id, name, new QuantityValue(0));

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

            item.Quantity(newQuantity);

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