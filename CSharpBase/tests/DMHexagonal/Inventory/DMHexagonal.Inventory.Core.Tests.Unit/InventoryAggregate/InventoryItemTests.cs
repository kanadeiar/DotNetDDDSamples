using DMHexagonal.Inventory.Core.Entries;
using DMHexagonal.Inventory.Core.InventoryAggregate;
using DMHexagonal.Inventory.Core.InventoryAggregate.Events;
using DMHexagonal.Inventory.Core.InventoryAggregate.Values;
using FluentAssertions;
using Kanadeiar.Common.Tests;

namespace DMHexagonal.Inventory.Core.Tests.Unit.InventoryAggregate;

public class InventoryItemTests
{
    [Theory(DisplayName = "Тестирование создания новой анкеты и события")]
    [InlineAutoMoqData(1, "Тест", 60)]
    public void TestCreate(int id, string name, int quantity)
    {
        var actual = InventoryItem.Create(new InventoryId(id), new InventoryNameValue(name), new QuantityValue(quantity));

        var entry = actual.Entry();
        entry.Id.Should().Be(id);
        entry.Name.Should().Be(name);
        entry.Quantity.Should().Be(quantity);
        var events = actual.Changes();
        events.Count().Should().Be(1);
        (events.Last() as InventoryCreated).Id.Id.Should().Be(id);
    }

    [Theory(DisplayName = "Тестирование нарушения инвариантов анкеты")]
    [InlineAutoMoqData(0, "Тест", 60)]
    [InlineAutoMoqData(1, "Т", 60)]
    [InlineAutoMoqData(1, "Тест", -1)]
    public void TestCreateNewQuestionnaire_WhenInvariantError(int id, string name, int quantity)
    {
        var act = () =>
        {
            _ = InventoryItem.Create(new InventoryId(id), new InventoryNameValue(name), new QuantityValue(quantity));
        };

        act.Should().Throw<ApplicationException>();
    }

    [Theory(DisplayName = "Проверка того, что можно переименовать любой предмет на складе")]
    [AutoMoqData]
    public void TestRename(InventoryEntry entry)
    {
        var expected = new InventoryNameValue("Новое имя");
        var sut = InventoryItem.Restore(entry);

        sut.Rename(expected);

        var actualEntry = sut.Entry();
        actualEntry.Name.Should().Be(expected.Name);
        var events = sut.Changes();
        events.Count().Should().Be(1);
        (events.Last() as InventoryRenamed).Name.Should().Be(expected);
    }

    [Theory(DisplayName = "Проверка того, что можно изменить количество элементов любого предмета на складе")]
    [AutoMoqData]
    public void TestQuantity(InventoryEntry entry)
    {
        var expected = new QuantityValue(44);
        var sut = InventoryItem.Restore(entry);

        sut.Quantity(expected);

        var actualEntry = sut.Entry();
        actualEntry.Quantity.Should().Be(expected.Quantity);
        var events = sut.Changes();
        events.Count().Should().Be(1);
        (events.Last() as InventoryQuantityChanged).Quantity.Should().Be(expected);
    }
}