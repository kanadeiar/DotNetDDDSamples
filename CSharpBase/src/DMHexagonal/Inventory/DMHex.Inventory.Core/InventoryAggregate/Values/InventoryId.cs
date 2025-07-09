using DMHex.Inventory.Core.Base.Abstractions;
using Kanadeiar.Common.Functionals;

namespace DMHex.Inventory.Core.InventoryAggregate.Values;

public record InventoryId(int Id) : IId
{
    public int Id { get; } = Id.Require(Id > 0, () =>
        throw new ApplicationException("Номер идентификатора должен быть положительным числом"));

    public override string ToString() => Id.ToString();
}