using Kanadeiar.Common;
using Kanadeiar.Common.Functionals;
using SampleDMConsoleApp.Helpers;

ConsoleHelper.PrintHeader("Образец основного поддомена на языке C#.", "Предметно-ориентированное проектирование на платформе .NET. Образцы приложений.");
ConsoleHelper.PrintLine("Образец: модель предметной области, порты и адаптеры и пирамида тестирования.");

var script = GeneralHelper.CreateScript();

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

