using Kanadeiar.Common;
using SampleESConsoleApp.Helpers;

ConsoleHelper.PrintHeader("Образец основного поддомена на языке C#.", "Предметно-ориентированное проектирование на платформе .NET. Образцы приложений.");
ConsoleHelper.PrintLine("Образец: модель предметной области, основанная на событиях, CQRS и пирамида тестирования.");

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


ConsoleHelper.PrintFooter();
