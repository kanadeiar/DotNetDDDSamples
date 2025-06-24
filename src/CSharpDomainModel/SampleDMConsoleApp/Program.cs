using DomainModel.Inventory.Application.Develop;
using DomainModel.Inventory.Application.InventoryFeature;
using DomainModel.Inventory.Infra.Adapters;
using DomainModel.Inventory.Infra.Tools;
using Kanadeiar.Common;

ConsoleHelper.PrintHeader("Образец вспомогательного поддомена на языке C#.", "Предметно-ориентированное проектирование на платформе .NET. Образцы приложений.");
ConsoleHelper.PrintLine("Образец: модель предметной области, порты и адаптеры и пирамида тестирования.");

var storage = new InventoryStorage();
var dispatcher = new DomainEventDispatcher();
var script = new InventoryScript(storage, dispatcher);
DeveloperScript.RunExample(dispatcher);
dispatcher.Run();

script.InitDemo()
    .Throw(fail => throw new ApplicationException(fail.Error));

ConsoleHelper.PrintLine("Все элементы:");
var items = script.AllItems()
    .TryGetValue(fail => throw new ApplicationException(fail.Error));
foreach (var text in items)
{
    ConsoleHelper.PrintLine(text);
}

var id = script.AddItem("newItem")
    .TryGetValue(fail => throw new ApplicationException(fail.Error));
script.ChangeName(id.Id, "Changed name")
    .Throw(fail => throw new ApplicationException(fail.Error));
script.ChangeQuantity(id.Id, 44)
    .Throw(fail => throw new ApplicationException(fail.Error));

ConsoleHelper.PrintLine("Все элементы после изменений:");
var updatedItems = script.AllItems()
    .TryGetValue(fail => throw new ApplicationException(fail.Error));
foreach (var text in updatedItems)
{
    ConsoleHelper.PrintLine(text);
}

ConsoleHelper.PrintFooter();
