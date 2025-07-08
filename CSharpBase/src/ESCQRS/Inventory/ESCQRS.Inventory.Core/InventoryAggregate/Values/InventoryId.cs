using ESCQRS.Inventory.Core.Abstractions.Base.Abstractions;
using Kanadeiar.Common.Functionals;

namespace ESCQRS.Inventory.Core.InventoryAggregate.Values;

public record InventoryId(Guid Id) : IId
{
    public static InventoryId New => new(Guid.NewGuid());

    public Guid Id { get; } = Id.Require(Id != default, () =>
        throw new ApplicationException("Номер идентификатора должен быть обязательно назначен"));

    public override string ToString() => Id.ToString();
}