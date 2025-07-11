using Kanadeiar.Common;
using Kanadeiar.Common.Functionals;
using TS3LayersConsoleApp.Helpers;

ConsoleHelper.PrintHeader("Образец основного поддомена на языке C#.", "Предметно-ориентированное проектирование на платформе .NET. Примеры приложений.");
ConsoleHelper.PrintLine("Образец: транзакционный сценарий, трехслойная архитектура и перевернутая пирамида тестирования.");

var script = GeneralApplicationHelper.CreateScript();

script.InitDemo()
    .Throw(fail => throw new ApplicationException(fail.Error));

ConsoleHelper.Pause();

ConsoleHelper.PrintLine("Все элементы:");
var items = script.AllItems()
    .TryGetValue(fail => throw new ApplicationException(fail.Error));
foreach (var text in items)
{
    ConsoleHelper.PrintLine(text.ToString());
}

ConsoleHelper.PrintLine("Изменения в данных ...");

var id = script.AddItem("Новый")
    .Throw(fail => throw new ApplicationException(fail.Error));
script.ChangeName(id, "Обновленное имя")
    .Throw(fail => throw new ApplicationException(fail.Error));
script.ChangeQuantity(id, 33)
    .Throw(fail => throw new ApplicationException(fail.Error));

ConsoleHelper.Pause();

ConsoleHelper.PrintLine("Все элементы после изменений:");
items = script.AllItems()
    .TryGetValue(fail => throw new ApplicationException(fail.Error));
foreach (var text in items)
{
    ConsoleHelper.PrintLine(text.ToString());
}

ConsoleHelper.PrintFooter();
