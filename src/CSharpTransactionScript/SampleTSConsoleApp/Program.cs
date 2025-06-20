using Kanadeiar.Common;
using TransactionScript.Inventory.DataAccessLayer.Data;
using TransactionScript.Inventory.PresentationLayer.Scripts;

ConsoleHelper.PrintHeader("Образец вспомогательного поддомена на языке C#.", "Предметно-ориентированное проектирование на платформе .NET. Образцы приложений.");
ConsoleHelper.PrintLine("Образец: транзакционный сценарий, трехслойная архитектура и перевернутая пирамида тестирования.");

var storage = new InventoryEntriesStorage();
var script = new InventoryScript(storage);
script.InitDemo()
    .Throw(fail => throw new ApplicationException(fail.Error));

Console.WriteLine("Все:");
var items = script.AllItems()
    .TryGetValue(fail => throw new ApplicationException(fail.Error)).ToArray();
Array.ForEach(items, Console.WriteLine);

var id = script.AddItem("newItem")
    .TryGetValue(fail => throw new ApplicationException(fail.Error));
script.ChangeName(id, "Changed name")
    .Throw(fail => throw new ApplicationException(fail.Error));
script.ChangeQuantity(id, 44)
    .Throw(fail => throw new ApplicationException(fail.Error));

Console.WriteLine("Все после изменения:");
items = script.AllItems()
    .TryGetValue(fail => throw new ApplicationException(fail.Error)).ToArray();
Array.ForEach(items, Console.WriteLine);

ConsoleHelper.PrintFooter();
