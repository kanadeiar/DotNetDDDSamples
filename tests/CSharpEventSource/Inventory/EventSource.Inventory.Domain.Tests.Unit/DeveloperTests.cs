using FluentAssertions;

namespace EventSource.Inventory.Domain.Tests.Unit;

public class DeveloperTests
{
    [Fact]
    public void TestDev()
    {
        true.Should().BeTrue();
    }
}