using EventSource.Inventory.Contracts.Abstractions;
using Kanadeiar.Common;

namespace EventSource.Inventory.Domain.InventoryAggregate.Values;

public record InventoryId(Guid Id) : IId
{
    public static InventoryId New => new(Guid.NewGuid());

    public Guid Id { get; } = Id.Require(Id != default, () =>
        throw new ApplicationException("Номер идентификатора должен быть назначен"));

    public override string ToString() => Id.ToString();
}