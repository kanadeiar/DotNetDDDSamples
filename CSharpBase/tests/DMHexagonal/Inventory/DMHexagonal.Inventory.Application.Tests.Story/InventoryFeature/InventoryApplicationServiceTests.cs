using DMHexagonal.Inventory.Application.InventoryFeature;
using DMHexagonal.Inventory.Application.Ports;
using DMHexagonal.Inventory.Core.InventoryAggregate;
using DMHexagonal.Inventory.Core.InventoryAggregate.Values;
using FluentAssertions;
using Kanadeiar.Common.Functionals;
using Kanadeiar.Common.Tests;
using Moq;

namespace DMHexagonal.Inventory.Application.Tests.Story.InventoryFeature;

public class InventoryApplicationServiceTests
{
    [Theory(DisplayName = "История: Я, как пользователь, могу просмотреть в удобном виде демонстрационные данные.")]
    [AutoMoqData]
    public void TestAllItems(InventoryItem[] items, Mock<IInventoryStorage> storageMock)
    {
        storageMock.Setup(x => x.All())
            .Returns(items);
        var sut = new InventoryApplicationService(storageMock.Object);

        var actuals = sut.AllItems()
            .TryGetValue(_ => throw new ApplicationException());

        actuals.Count().Should().Be(3);
        actuals.First().ToString().Should().Be(items.First().Entry().Name + " - " + items.First().Entry().Quantity + " шт.");
    }

    [Theory(DisplayName = "История: Я, как пользователь, могу отредактировать название любого элемента.")]
    [AutoMoqData]
    public void TestRename(InventoryItem item, Mock<IInventoryStorage> storageMock)
    {
        var expected = new InventoryNameValue("Новое имя");
        InventoryItem actual = null;
        storageMock.Setup(x => x.Load(It.IsAny<InventoryId>()))
            .Returns(item);
        storageMock.Setup(x => x.Save(It.IsAny<InventoryItem>()))
            .Callback<InventoryItem>(i => { actual = i; });
        var sut = new InventoryApplicationService(storageMock.Object);

        var result = sut.ChangeName(new InventoryId(1), expected);

        result.Should().BeOfType<Result>();
        actual.Id.Should().Be(item.Id);
        actual.Entry().Name.Should().Be(expected.Name);
        storageMock.Verify(x => x.Save(It.IsAny<InventoryItem>()), Times.Once);
    }

    [Theory(DisplayName = "История: Я, как пользователь, могу изменить количество любого элемента.")]
    [AutoMoqData]
    public void TestQuantity(InventoryItem item, Mock<IInventoryStorage> storageMock)
    {
        var expected = new QuantityValue(22);
        InventoryItem actual = null;
        storageMock.Setup(x => x.Load(It.IsAny<InventoryId>()))
            .Returns(item);
        storageMock.Setup(x => x.Save(It.IsAny<InventoryItem>()))
            .Callback<InventoryItem>(i => { actual = i; });
        var sut = new InventoryApplicationService(storageMock.Object);

        var result = sut.ChangeQuantity(new InventoryId(1), expected);

        result.Should().BeOfType<Result>();
        actual.Id.Should().Be(item.Id);
        actual.Entry().Quantity.Should().Be(expected.Quantity);
        storageMock.Verify(x => x.Save(It.IsAny<InventoryItem>()), Times.Once);
    }
}