using Kanadeiar.Common.Functionals;

namespace EventSource.Inventory.Domain.InventoryAggregate.Values;

public record QuantityValue(int Quantity)
{
    public int Quantity { get; } = Quantity.Require(Quantity is >= 0 and <= 1000, () =>
        throw new ApplicationException("Количество должно быть от 0 до 1000 штук"));

    public override string ToString() => Quantity.ToString();
}