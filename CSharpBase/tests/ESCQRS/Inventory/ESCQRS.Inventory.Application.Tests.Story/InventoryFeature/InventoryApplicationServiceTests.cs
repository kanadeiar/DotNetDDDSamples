using ESCQRS.Inventory.Application.InventoryFeature;
using ESCQRS.Inventory.Application.ReadModel;
using ESCQRS.Inventory.Application.ReadModel.Ports;
using ESCQRS.Inventory.Core.Base;
using ESCQRS.Inventory.Core.Base.Abstractions;
using ESCQRS.Inventory.Core.InventoryAggregate;
using ESCQRS.Inventory.Core.InventoryAggregate.Events;
using ESCQRS.Inventory.Core.InventoryAggregate.Values;
using ESCQRS.Inventory.Core.ReadModel;
using FluentAssertions;
using Kanadeiar.Common.Functionals;
using Kanadeiar.Common.Tests;
using Moq;

namespace ESCQRS.Inventory.Application.Tests.Story.InventoryFeature;

public class InventoryApplicationServiceTests
{
    [Theory(DisplayName = "История: Я, как пользователь, могу просмотреть в удобном виде демонстрационные данные.")]
    [AutoMoqData]
    public void TestAllItems(InventoryProjection[] projections, Mock<IStorage<InventoryItem>> storageMock, Mock<IReadModelStorage> readMock, Mock<IDispatcher> dispatcherMock)
    {
        var readSut = new ReadModelMaster(readMock.Object);
        readSut.Init(dispatcherMock.Object);
        var sut = new InventoryApplicationService(storageMock.Object, readSut);
        sut.InitDemo().Should().BeOfType<Result>();
        readMock.SetupGet(x => x.Inventories)
            .Returns(projections.ToList());

        var actuals = sut.AllItems()
            .TryGetValue(_ => throw new ApplicationException());

        storageMock.Verify(x => x.Save(It.IsAny<InventoryItem>()), Times.Exactly(3));
        actuals.Count().Should().Be(3);
        actuals.First().ToString().Should().Be(projections.First().ToString());
    }

    [Theory(DisplayName = "История: Я, как пользователь, могу отредактировать название любого элемента.")]
    [AutoMoqData]
    public void TestRename(InventoryId id, InventoryProjection[] projections, Mock<IStorage<InventoryItem>> storageMock, Mock<IReadModelStorage> readMock/*, Mock<IDispatcher> dispatcherMock*/)
    {
        var expected = new InventoryNameValue("Новое имя");
        var item = InventoryItem.Create(id, new InventoryNameValue("name"), new QuantityValue(0));
        EventAggregateRoot actual = null;
        storageMock.Setup(x => x.Load(It.IsAny<InventoryId>()))
            .Returns(item);
        storageMock.Setup(x => x.Save(It.IsAny<InventoryItem>()))
            .Callback<EventAggregateRoot>(i => { actual = i; });
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
        var item = InventoryItem.Create(id, new InventoryNameValue("name"), new QuantityValue(0));
        EventAggregateRoot actual = null;
        storageMock.Setup(x => x.Load(It.IsAny<InventoryId>()))
            .Returns(item);
        storageMock.Setup(x => x.Save(It.IsAny<InventoryItem>()))
            .Callback<EventAggregateRoot>(i => { actual = i; });
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