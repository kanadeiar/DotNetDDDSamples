using Kanadeiar.Common;
using SampleESConsoleApp.Helpers;
using SampleESConsoleApp.Scripts;

ConsoleHelper.PrintHeader("Образец основного поддомена на языке C#.", "Предметно-ориентированное проектирование на платформе .NET. Образцы приложений.");
ConsoleHelper.PrintLine("Образец: модель предметной области, основанная на событиях, CQRS и пирамида тестирования.");

var service = GeneralApplicationHelper.CreateService();
var script = new InventoryPresentationScript(service);

script.InitDemo();

ConsoleHelper.Pause();

script.PrintAllItems();

script.AddItem("Новый");
script.ChangeName("Новое имя");
script.ChangeQuantity(33);

ConsoleHelper.PrintLine("Нажать любую кнопку для изменений");
ConsoleHelper.Pause();

script.PrintAllItems("Все элементы после изменений:");

ConsoleHelper.PrintFooter();
