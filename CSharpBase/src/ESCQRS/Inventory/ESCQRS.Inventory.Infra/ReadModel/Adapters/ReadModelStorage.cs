using ESCQRS.Inventory.Application.ReadModel.Ports;
using ESCQRS.Inventory.Core.ReadModel;

namespace ESCQRS.Inventory.Infra.ReadModel.Adapters;

public class ReadModelStorage : IReadModelStorage
{
    public List<InventoryProjection> Inventories { get; } = [];
}