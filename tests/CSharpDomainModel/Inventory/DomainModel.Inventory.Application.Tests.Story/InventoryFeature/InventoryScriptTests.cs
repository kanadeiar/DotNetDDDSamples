using DomainModel.Inventory.Application.InventoryFeature;
using DomainModel.Inventory.Application.Ports;
using DomainModel.Inventory.Contracts.Abstractions;
using DomainModel.Inventory.Contracts.Base;
using DomainModel.Inventory.Domain.InventoryAggregate;
using DomainModel.Inventory.Domain.InventoryAggregate.Events;
using DomainModel.Inventory.Domain.InventoryAggregate.Values;
using FluentAssertions;
using Kanadeiar.Common;
using Kanadeiar.Tests;
using Moq;

namespace DomainModel.Inventory.Application.Tests.Story.InventoryFeature;

public class InventoryScriptTests
{
    [Theory(DisplayName = "История: Я, как пользователь, могу просмотреть в удобном виде демонстрационные данные.")]
    [AutoMoqData]
    public void TestAllItems(InventoryItem[] items, Mock<IInventoryStorage> storageMock, Mock<IDispatcher> dispatcherMock)
    {
        storageMock.Setup(x => x.All())
            .Returns(items);
        var sut = new InventoryScript(storageMock.Object, dispatcherMock.Object);

        var actuals = sut.AllItems()
            .TryGetValue(_ => throw new ApplicationException());

        actuals.Count().Should().Be(3);
        actuals.First().Should().Be(items.First().Entry().Name + " - " + items.First().Entry().Quantity + " шт.");
    }

    [Theory(DisplayName = "История: Я, как пользователь, могу отредактировать название любого элемента.")]
    [AutoMoqData]
    public void TestRename(InventoryItem item, Mock<IInventoryStorage> storageMock, Mock<IDispatcher> dispatcherMock)
    {
        var expected = new InventoryNameValue("Новое имя");
        InventoryItem actual = null;
        IEnumerable<DomainEvent> actualEvents = null;
        storageMock.Setup(x => x.Load(It.IsAny<InventoryId>()))
            .Returns(new Result<InventoryItem>(item));
        storageMock.Setup(x => x.Save(It.IsAny<InventoryItem>()))
            .Callback<InventoryItem>(i => { actual = i; });
        dispatcherMock.Setup(x => x.Dispatch(It.IsAny<IEnumerable<DomainEvent>>()))
            .Callback<IEnumerable<DomainEvent>>(evs => { actualEvents = evs; });
        var sut = new InventoryScript(storageMock.Object, dispatcherMock.Object);

        var result = sut.ChangeName(1, expected.Name);

        result.Should().BeOfType<Result>();
        actual.AggregateId.Should().Be(item.AggregateId);
        actual.Entry().Name.Should().Be(expected.Name);
        actualEvents.Count().Should().Be(1);
        ((InventoryRenamed)actualEvents.First()).Name.Should().Be(expected);
        storageMock.Verify(x => x.Save(It.IsAny<InventoryItem>()), Times.Once);
        dispatcherMock.Verify(x => x.Dispatch(It.IsAny<IEnumerable<DomainEvent>>()), Times.Once);
    }

    [Theory(DisplayName = "История: Я, как пользователь, могу изменить количество любого элемента.")]
    [AutoMoqData]
    public void TestQuantity(InventoryItem item, Mock<IInventoryStorage> storageMock, Mock<IDispatcher> dispatcherMock)
    {
        var expected = new QuantityValue(22);
        InventoryItem actual = null;
        IEnumerable<DomainEvent> actualEvents = null;
        storageMock.Setup(x => x.Load(It.IsAny<InventoryId>()))
            .Returns(new Result<InventoryItem>(item));
        storageMock.Setup(x => x.Save(It.IsAny<InventoryItem>()))
            .Callback<InventoryItem>(i => { actual = i; });
        dispatcherMock.Setup(x => x.Dispatch(It.IsAny<IEnumerable<DomainEvent>>()))
            .Callback<IEnumerable<DomainEvent>>(evs => { actualEvents = evs; });
        var sut = new InventoryScript(storageMock.Object, dispatcherMock.Object);

        var result = sut.ChangeQuantity(1, expected.Quantity);

        result.Should().BeOfType<Result>();
        actual.AggregateId.Should().Be(item.AggregateId);
        actual.Entry().Quantity.Should().Be(expected.Quantity);
        actualEvents.Count().Should().Be(1);
        ((InventoryQuantityChanged)actualEvents.First()).Quantity.Should().Be(expected);
        storageMock.Verify(x => x.Save(It.IsAny<InventoryItem>()), Times.Once);
        dispatcherMock.Verify(x => x.Dispatch(It.IsAny<IEnumerable<DomainEvent>>()), Times.Once);
    }
}