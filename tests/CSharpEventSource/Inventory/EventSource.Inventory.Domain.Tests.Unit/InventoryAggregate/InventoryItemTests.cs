using EventSource.Inventory.Contracts.Base;
using EventSource.Inventory.Domain.InventoryAggregate;
using EventSource.Inventory.Domain.InventoryAggregate.Events;
using EventSource.Inventory.Domain.InventoryAggregate.Values;
using FluentAssertions;
using Kanadeiar.Common.Tests;

namespace EventSource.Inventory.Domain.Tests.Unit.InventoryAggregate;

public class InventoryItemTests
{
    [Theory(DisplayName = "Тестирование создания новой анкеты и события")]
    [InlineAutoMoqData("Тест", 60)]
    public void TestCreate(string name, int quantity)
    {
        var id = InventoryId.New;
        var actual = new InventoryItem(id, new InventoryNameValue(name), new QuantityValue(quantity));

        var events = actual.Changes();
        events.Count.Should().Be(1);
        ((InventoryCreated)events.First()).Id.Should().Be(id);
        ((InventoryCreated)events.First()).Name.Should().Be(new InventoryNameValue(name));
    }

    [Theory(DisplayName = "Тестирование нарушения инвариантов анкеты")]
    [InlineAutoMoqData("Т", 60)]
    [InlineAutoMoqData("Тест", -1)]
    public void TestCreateNewQuestionnaire_WhenInvariantError(string name, int quantity)
    {
        var act = () =>
        {
            _ = new InventoryItem(InventoryId.New, new InventoryNameValue(name), new QuantityValue(quantity));
        };

        act.Should().Throw<ApplicationException>();
    }

    [Theory(DisplayName = "Проверка того, что можно переименовать любой предмет на складе")]
    [AutoMoqData]
    public void TestRename(InventoryCreated ev, InventoryNameValue expected)
    {
        var stream = new EventStream([ev]);
        var sut = new InventoryItem();
        sut.Apply(stream);
        sut.Reset();

        sut.Rename(expected);

        var events = sut.Changes();
        events.Count.Should().Be(1);
        ((InventoryRenamed)events.First()).Name.Should().Be(expected);
    }

    [Theory(DisplayName = "Проверка того, что можно изменить количество элементов любого предмета на складе")]
    [AutoMoqData]
    public void TestQuantity(InventoryCreated ev, QuantityValue expected)
    {
        var stream = new EventStream([ev]);
        var sut = new InventoryItem();
        sut.Apply(stream);
        sut.Reset();

        sut.Quantity(expected);

        var events = sut.Changes();
        events.Count.Should().Be(1);
        ((InventoryQuantityChanged)events.First()).Quantity.Should().Be(expected);
    }
}