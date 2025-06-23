using FluentAssertions;

namespace ActiveRecord.Inventory.ServicesLayer.Tests.Integration;

public class DeveloperTests
{
    [Fact]
    public void TestDev()
    {
        true.Should().BeTrue();
    }
}