using ESCQRSConsoleApp.Helpers;
using ESCQRSConsoleApp.Scripts;
using Kanadeiar.Common;

ConsoleHelper.PrintHeader("Образец основного поддомена на языке C#.", "Предметно-ориентированное проектирование на платформе .NET. Примеры приложений.");
ConsoleHelper.PrintLine("Образец: модель предметной области, основанная на событиях, CQRS и пирамида тестирования.");

var service = GeneralApplicationHelper.CreateService();
var script = new InventoryPresentationScript(service);

script.InitDemo();

ConsoleHelper.Pause();

script.PrintAllItems();

ConsoleHelper.PrintLine("Изменения в данных ...");

script.AddItem("Новый");
script.ChangeName("Обновленное имя");
script.ChangeQuantity(33);

ConsoleHelper.Pause();

script.PrintAllItems("Все элементы после изменений:");

ConsoleHelper.PrintFooter();
