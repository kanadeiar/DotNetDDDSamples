using Kanadeiar.Common.Functionals;

namespace DomainModel.Inventory.Domain.InventoryAggregate.Values;

public record InventoryId(int Id)
{
    public int Id { get; } = Id.Require(Id > 0, () =>
        throw new ApplicationException("Номер идентификатора должен быть положительным числом"));

    public override string ToString() => Id.ToString();
}