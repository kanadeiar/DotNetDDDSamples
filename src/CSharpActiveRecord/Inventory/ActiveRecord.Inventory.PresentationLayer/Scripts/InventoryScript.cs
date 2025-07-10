using ActiveRecord.Inventory.ServicesLayer.Services;
using Kanadeiar.Common.Functionals;

namespace ActiveRecord.Inventory.PresentationLayer.Scripts;

public class InventoryScript
{
    private readonly InventoryApplicationService _service = new();

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
        return _service.AllInventoryItems();
    }

    public Result<int> AddItem(string name, int quantity = 0)
    {
        return _service.CreateNewInventoryItem(name, quantity);
    }

    public Result ChangeName(int id, string newName)
    {
        return _service.ChangeNameOfInventoryItem(id, newName);
    }

    public Result ChangeQuantity(int id, int newQuantity)
    {
        return _service.ChangeQuantityOfInventoryItem(id, newQuantity);
    }
}