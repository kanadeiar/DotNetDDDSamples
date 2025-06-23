using FluentAssertions;
using FrameworkConsoleApp1Tests.Infrastructure;
using Kanadeiar.Tests;
using TransactionScript.Inventory.DataAccessLayer.Contracts;
using TransactionScript.Inventory.MainLogicLayer.InventoryModule;

namespace TransactionScript.Inventory.MainLogicLayer.Tests.Unit.InventoryModule;

public class InventoryItemTests
{
    [Theory(DisplayName = "Тестирование создания нового предмета")]
    [InlineAutoMoqData(1, "Тестовое", 10)]
    public void TestCreateNewInventory(int id, string name, int quantity)
    {
        var actual = new InventoryItem(id, name, quantity);
        var entry = actual.Entry();
        entry.Id.Should().Be(id);
        entry.Name.Should().Be(name);
        entry.Quantity.Should().Be(quantity);
    }

    [Theory(DisplayName = "Тестирование нарушения инвариантов при создании одного предмета")]
    [InlineAutoMoqData(0, "Т", 10)]
    [InlineAutoMoqData(0, "Тестов", -1)]
    public void TestCreateNewInventory_WhenInvariantError(int id, string name, int quantity)
    {
        Action act = () => { _ = new InventoryItem(id, name, quantity); };

        act.Should().Throw<ApplicationException>();
    }

    [Theory(DisplayName = "Проверка того, что можно переименовать любой предмет на складе")]
    [AutoMoqData]
    public void TestRename(InventoryEntry entry)
    {
        var expected = "Новое имя";
        var sut = InventoryItem.Restore(entry);

        sut = sut.Rename(expected);

        var actual = sut.Entry();
        actual.Name.Should().Be(expected);
    }

    [Theory(DisplayName = "Проверка того, что можно изменить количество элементов любого предмета на складе")]
    [AutoMoqData]
    public void TestQuantity(InventoryEntry entry)
    {
        var expected = 44;
        var sut = InventoryItem.Restore(entry);

        sut = sut.Quantity(expected);

        var actual = sut.Entry();
        actual.Quantity.Should().Be(expected);
    }
}