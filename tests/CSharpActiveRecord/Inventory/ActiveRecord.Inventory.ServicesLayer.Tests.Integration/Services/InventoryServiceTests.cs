﻿using ActiveRecord.Inventory.DataAccessLayer;
using ActiveRecord.Inventory.DataAccessLayer.Contracts;
using ActiveRecord.Inventory.DataAccessLayer.Data;
using ActiveRecord.Inventory.MainLogicLayer.InventoryModule;
using ActiveRecord.Inventory.ServicesLayer.Services;
using AutoFixture.Xunit2;
using FluentAssertions;
using Kanadeiar.Common.Functionals;
using Kanadeiar.Common.Tests;

namespace ActiveRecord.Inventory.ServicesLayer.Tests.Integration.Services;

public class InventoryServiceTests
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

    [Theory(DisplayName = "Проверка возможности добавления нового элемента")]
    [AutoData]
    public void TestCreateNewInventoryItem(string name, int quantity)
    {
        var storage = new InventoryEntriesStorage();
        Registry.InitFake(new FakeRegistry{ FakeStorage = storage });
        var sut = new InventoryApplicationService();

        sut.CreateNewInventoryItem(name, quantity);

        storage.All().Count().Should().Be(1);
        var first = storage.All().First();
        first.Name.Should().Be(name);
        first.Quantity.Should().Be(quantity);
    }

    [Theory(DisplayName = "Проверка возможности получения элемента")]
    [AutoData]
    public void TestAllInventoryItems(InventoryEntry entry)
    {
        var storage = new InventoryEntriesStorage();
        entry = new InventoryEntry { Id = storage.NextIdentity(), Name = entry.Name, Quantity = entry.Quantity };
        storage.Save(entry);
        Registry.InitFake(new FakeRegistry { FakeStorage = storage });
        var sut = new InventoryApplicationService();

        var items = sut.AllInventoryItems()
            .Throw(f => new ApplicationException());

        items.Count().Should().Be(1);
        var first = items.First();
        first.Should().Be($"{entry.Name} - {entry.Quantity} шт.");
    }

    [Theory(DisplayName = "Проверка возможности изменения названия элемента")]
    [AutoData]
    public void TestChangeNameOfInventoryItem(InventoryEntry entry)
    {
        var expected = "newName";
        var storage = new InventoryEntriesStorage();
        entry = new InventoryEntry { Id = storage.NextIdentity(), Name = entry.Name, Quantity = entry.Quantity };
        storage.Save(entry);
        Registry.InitFake(new FakeRegistry { FakeStorage = storage });
        var sut = new InventoryApplicationService();

        var result = sut.ChangeNameOfInventoryItem(entry.Id, expected);

        result.Should().BeOfType<Result>();
        var first = storage.All().First();
        first.Name.Should().Be(expected);
    }

    [Theory(DisplayName = "Проверка возможности изменения количества элемента")]
    [AutoData]
    public void TestChangeQuantityOfInventoryItem(InventoryEntry entry)
    {
        var expected = 333;
        var storage = new InventoryEntriesStorage();
        entry = new InventoryEntry { Id = storage.NextIdentity(), Name = entry.Name, Quantity = entry.Quantity };
        storage.Save(entry);
        Registry.InitFake(new FakeRegistry { FakeStorage = storage });
        var sut = new InventoryApplicationService();

        var result = sut.ChangeQuantityOfInventoryItem(entry.Id, expected);

        result.Should().BeOfType<Result>();
        var first = storage.All().First();
        first.Quantity.Should().Be(expected);
    }
}