using EventSource.Inventory.Application.InventoryFeature;
using EventSource.Inventory.Application.Ports;
using EventSource.Inventory.Application.ReadModel;
using EventSource.Inventory.Contracts.Abstractions;
using EventSource.Inventory.Contracts.Base;
using EventSource.Inventory.Domain.InventoryAggregate;
using EventSource.Inventory.Domain.InventoryAggregate.Events;
using EventSource.Inventory.Domain.InventoryAggregate.Values;
using EventSource.Inventory.Domain.ReadModel;
using FluentAssertions;
using Kanadeiar.Common.Functionals;
using Kanadeiar.Common.Tests;
using Moq;

namespace EventSource.Inventory.Application.Tests.Story.InventoryFeature;

public class InventoryScriptTests
{
    [Theory(DisplayName = "История: Я, как пользователь, могу просмотреть в удобном виде демонстрационные данные.")]
    [AutoMoqData]
    public void TestAllItems(InventoryProjection[] projections, Mock<IStorage<InventoryItem>> storageMock, Mock<IReadModelStorage> readMock, Mock<IDispatcher> dispatcherMock)
    {
        var readSut = new ReadModelMaster(readMock.Object);
        readSut.Init(dispatcherMock.Object);
        var sut = new InventoryApplicationService(storageMock.Object, readSut);
        sut.InitDemo().Should().BeOfType<Result>();
        readMock.SetupGet(x => x.All)
            .Returns(projections.ToList());

        var actuals = sut.AllItems()
            .TryGetValue(_ => throw new ApplicationException());
        
        storageMock.Verify(x => x.Save(It.IsAny<AggregateRoot>()), Times.Exactly(3));
        actuals.Count().Should().Be(3);
        actuals.First().Should().Be(projections.First().ToString());
    }

    [Theory(DisplayName = "История: Я, как пользователь, могу отредактировать название любого элемента.")]
    [AutoMoqData]
    public void TestRename(InventoryId id, InventoryProjection[] projections, Mock<IStorage<InventoryItem>> storageMock, Mock<IReadModelStorage> readMock/*, Mock<IDispatcher> dispatcherMock*/)
    {
        var expected = new InventoryNameValue("Новое имя");
        var item = new InventoryItem(id, new InventoryNameValue("name"), new QuantityValue(0));
        AggregateRoot actual = null;
        storageMock.Setup(x => x.Load(It.IsAny<InventoryId>()))
            .Returns(item);
        storageMock.Setup(x => x.Save(It.IsAny<AggregateRoot>()))
            .Callback<AggregateRoot>(i => { actual = i; });
        var sut = new InventoryApplicationService(storageMock.Object, new ReadModelMaster(readMock.Object));

        var result = sut.ChangeName(id, expected);

        result.Should().BeOfType<Result>();
        actual.Id.Should().Be(item.Id);
        var events = actual.Changes();
        events.Count.Should().Be(2);
        ((InventoryRenamed)events.Last()).Name.Should().Be(expected);
        storageMock.Verify(x => x.Save(It.IsAny<InventoryItem>()), Times.Once);
    }

    [Theory(DisplayName = "История: Я, как пользователь, могу изменить количество любого элемента.")]
    [AutoMoqData]
    public void TestQuantity(InventoryId id, InventoryProjection[] projections, Mock<IStorage<InventoryItem>> storageMock, Mock<IReadModelStorage> readMock, Mock<IDispatcher> dispatcherMock)
    {
        var expected = new QuantityValue(22);
        var item = new InventoryItem(id, new InventoryNameValue("name"), new QuantityValue(0));
        AggregateRoot actual = null;
        storageMock.Setup(x => x.Load(It.IsAny<InventoryId>()))
            .Returns(item);
        storageMock.Setup(x => x.Save(It.IsAny<AggregateRoot>()))
            .Callback<AggregateRoot>(i => { actual = i; });
        var sut = new InventoryApplicationService(storageMock.Object, new ReadModelMaster(readMock.Object));

        var result = sut.ChangeQuantity(id, expected);

        result.Should().BeOfType<Result>();
        actual.Id.Should().Be(item.Id);
        var events = actual.Changes();
        events.Count.Should().Be(2);
        ((InventoryQuantityChanged)events.Last()).Quantity.Should().Be(expected);
        storageMock.Verify(x => x.Save(It.IsAny<InventoryItem>()), Times.Once);
    }
}