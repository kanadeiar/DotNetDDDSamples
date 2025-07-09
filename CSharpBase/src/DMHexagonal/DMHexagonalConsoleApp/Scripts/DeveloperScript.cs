using DMHexagonal.Inventory.Core.Base.Abstractions;
using DMHexagonal.Inventory.Core.InventoryAggregate.Events;

namespace DMHexagonalConsoleApp.Scripts;

public class DeveloperScript
{
    public static void RunExampleSubscribe(IRegisterDispatcher dispatcher)
    {
        dispatcher.RegisterHandler<InventoryCreated>(ev =>
        {
            Console.WriteLine($"## Событие создания нового элемента c Id: {ev.Id} {ev.Name} - {ev.Quantity} шт.");
        });
    }
}