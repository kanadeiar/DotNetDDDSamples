﻿using ActiveRecord.Inventory.DataAccessLayer;
using ActiveRecord.Inventory.DataAccessLayer.Data;
using ActiveRecord.Inventory.PresentationLayer.Scripts;
using FluentAssertions;
using Kanadeiar.Common.Functionals;

namespace ActiveRecord.Inventory.PresentationLayer.Tests.Story.Scripts;

public class InventoryScriptTests
{
    [Fact(DisplayName = "История: Я, как пользователь, могу просмотреть в удобном виде демонстрационные данные.")]
    public void TestAllItems()
    {
        var expected = "Колбаса";
        var storage = new InventoryEntriesStorage();
        Registry.InitFake(new FakeRegistry { FakeStorage = storage });
        var sut = new InventoryScript();
        sut.InitDemo().Should().BeOfType<Result>();

        var actuals = sut.AllItems()
            .TryGetValue(_ => throw new ApplicationException());

        actuals.Count().Should().Be(3);
        actuals.First().Should().Be(expected + " - 0 шт.");
    }

    [Fact(DisplayName = "История: Я, как пользователь, могу отредактировать название любого элемента.")]
    public void TestRename()
    {
        var expected = "Новое название";
        var storage = new InventoryEntriesStorage();
        Registry.InitFake(new FakeRegistry { FakeStorage = storage });
        var sut = new InventoryScript();
        sut.InitDemo().Should().BeOfType<Result>();

        var result = sut.ChangeName(1, expected);

        result.Should().BeOfType<Result>();
        var actuals = storage.All();
        actuals.First().Name.Should().Be(expected);
    }

    [Fact(DisplayName = "История: Я, как пользователь, могу изменить количество любого элемента.")]
    public void TestQuantity()
    {
        var expected = 22;
        var storage = new InventoryEntriesStorage();
        Registry.InitFake(new FakeRegistry { FakeStorage = storage });
        var sut = new InventoryScript();
        sut.InitDemo().Should().BeOfType<Result>();

        var result = sut.ChangeQuantity(1, expected);

        result.Should().BeOfType<Result>();
        var actuals = storage.All();
        actuals.Count().Should().Be(3);
        actuals.First().Quantity.Should().Be(expected);
    }
}