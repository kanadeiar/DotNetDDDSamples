using Kanadeiar.Common.Functionals;

namespace ESCQRS.Inventory.Core.InventoryAggregate.Values;

public record InventoryNameValue(string Name)
{
    public string Name { get; } = Name.Require(Name.Length is >= 3 and <= 90, () =>
        throw new ApplicationException("Имя должно быть длинной от 3 до 90 символов"));

    public override string ToString() => $"{Name}";
}