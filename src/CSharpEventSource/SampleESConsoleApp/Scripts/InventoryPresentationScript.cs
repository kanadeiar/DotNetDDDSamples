using EventSource.Inventory.Application.InventoryFeature;
using EventSource.Inventory.Contracts.Abstractions;
using EventSource.Inventory.Domain.InventoryAggregate.Values;
using Kanadeiar.Common;
using Kanadeiar.Common.Functionals;

namespace SampleESConsoleApp.Scripts;

public class InventoryPresentationScript(InventoryApplicationService service)
{
    private InventoryId _id;

    public void InitDemo()
    {
        service.InitDemo()
            .Throw(fail => throw new ApplicationException(fail.Error));
    }

    public void PrintAllItems(string message = "Все элементы:")
    {
        ConsoleHelper.PrintLine(message);
        var items = service.AllItems()
            .TryGetValue(fail => throw new ApplicationException(fail.Error));
        foreach (var text in items)
        {
            ConsoleHelper.PrintLine(text.ToString());
        }
    }

    public void AddItem(string name)
    {
        var id = service.AddItem(new InventoryNameValue(name))
            .TryGetValue(fail => throw new ApplicationException(fail.Error));
        _id = id;
    }

    public void ChangeName(string newName)
    {
        service.ChangeName(_id, new InventoryNameValue(newName))
            .Throw(fail => throw new ApplicationException(fail.Error));

    }

    public void ChangeQuantity(int newQuantity)
    {
        service.ChangeQuantity(_id, new QuantityValue(newQuantity))
            .Throw(fail => throw new ApplicationException(fail.Error));
    }
}