using AutoFixture.Xunit2;
using ESCQRS.Inventory.Core.InventoryAggregate.Events;
using ESCQRS.Inventory.Core.InventoryAggregate.Values;
using ESCQRS.Inventory.Core.ReadModel;
using FluentAssertions;
using Kanadeiar.Common.Tests;

namespace ESCQRS.Inventory.Core.Tests.Unit.ReadModel;

public class InventoryProjectionTests
{
    [Theory(DisplayName = "Проверка события создания элемента")]
    [InlineAutoMoqData("Тест", 60)]
    public void TestInventoryCreate(string name, int quantity)
    {
        var expectedId = Guid.NewGuid();
        var ev = new InventoryCreated(new InventoryId(expectedId), new InventoryNameValue(name),
            new QuantityValue(quantity));

        var projection = new InventoryProjection(ev);

        projection.Id.Id.Should().Be(expectedId);
        projection.ToString().Should().Be($"{name} - {quantity} шт.");
    }

    [Theory(DisplayName = "Проверка события переименования элемента")]
    [AutoData]
    public void TestInventoryRenamed(string expected)
    {
        var expectedId = Guid.NewGuid();
        var created = new InventoryCreated(new InventoryId(expectedId), new InventoryNameValue("name"),
            new QuantityValue(0));
        var ev = new InventoryRenamed(new InventoryId(expectedId), new InventoryNameValue(expected));
        var sut = new InventoryProjection(created);

        sut = sut.Apply(ev);

        sut.ToString().Should().Be($"{expected} - 0 шт.");
    }

    [Theory(DisplayName = "Проверка события переименования элемента")]
    [AutoData]
    public void TestInventoryQuantityChanged(int expected)
    {
        var expectedId = Guid.NewGuid();
        var created = new InventoryCreated(new InventoryId(expectedId), new InventoryNameValue("name"),
            new QuantityValue(0));
        var ev = new InventoryQuantityChanged(new InventoryId(expectedId), new QuantityValue(expected));
        var sut = new InventoryProjection(created);

        sut = sut.Apply(ev);

        sut.ToString().Should().Be($"name - {expected} шт.");
    }
}