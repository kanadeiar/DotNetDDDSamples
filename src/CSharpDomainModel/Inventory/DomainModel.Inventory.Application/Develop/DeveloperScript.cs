using DomainModel.Inventory.Contracts.Abstractions;
using DomainModel.Inventory.Domain.InventoryAggregate.Events;

namespace DomainModel.Inventory.Application.Develop;

public static class DeveloperScript
{
    public static void RunExample(IDispatcher dispatcher)
    {
        dispatcher.RegisterHandler<InventoryCreated>(ev =>
        {
            Console.WriteLine("## Событие создания нового элемента ## id:" + ev.Id + " " + ev.OccurredOn);
        });
    }
}