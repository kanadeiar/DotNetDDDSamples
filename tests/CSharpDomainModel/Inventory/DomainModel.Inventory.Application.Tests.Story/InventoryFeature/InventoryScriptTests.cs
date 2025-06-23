using DomainModel.Inventory.Application.InventoryFeature;
using DomainModel.Inventory.Application.Ports;
using DomainModel.Inventory.Contracts.Abstractions;
using DomainModel.Inventory.Domain.InventoryAggregate;
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
}