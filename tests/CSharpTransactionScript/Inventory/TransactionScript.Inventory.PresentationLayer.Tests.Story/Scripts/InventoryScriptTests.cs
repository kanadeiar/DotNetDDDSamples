using FluentAssertions;
using Kanadeiar.Common;
using TransactionScript.Inventory.DataAccessLayer.Data;
using TransactionScript.Inventory.PresentationLayer.Scripts;

namespace TransactionScript.Inventory.PresentationLayer.Tests.Story.Scripts;

public class InventoryScriptTests
{
    [Fact(DisplayName = "История: Я, как пользователь, могу просмотреть в удобном виде демонстрационные данные.")]
    public void TestAllItems()
    {
        var expected = "Колбаса";
        var storage = new InventoryStorage();
        var sut = new InventoryScript(storage);
        sut.InitDemo();

        var actuals = sut.AllItems()
            .TryGetValue(_ => throw new ApplicationException());

        actuals.Count().Should().Be(3);
        actuals.First().Should().Be(expected + " - 0 шт.");
    }

    [Fact(DisplayName = "История: Я, как пользователь, могу отредактировать название любого элемента.")]
    public void TestRename()
    {
        var expected = "Новое название";
        var storage = new InventoryStorage();
        var sut = new InventoryScript(storage);
        sut.InitDemo();

        var result = sut.ChangeName(0, expected);

        result.Should().BeOfType<Result>();
        var actuals = storage.All();
        actuals.Count().Should().Be(3);
        actuals.First().Name.Should().Be(expected);
    }

    [Fact(DisplayName = "История: Я, как пользователь, могу изменить количество любого элемента.")]
    public void TestQuantity()
    {
        var expected = 22;
        var storage = new InventoryStorage();
        var sut = new InventoryScript(storage);
        sut.InitDemo();

        var result = sut.ChangeQuantity(0, expected);

        result.Should().BeOfType<Result>();
        var actuals = storage.All();
        actuals.Count().Should().Be(3);
        actuals.First().Quantity.Should().Be(expected);
    }
}