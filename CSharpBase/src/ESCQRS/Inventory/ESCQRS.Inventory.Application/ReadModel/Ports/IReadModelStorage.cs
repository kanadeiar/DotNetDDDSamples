using ESCQRS.Inventory.Core.ReadModel;

namespace ESCQRS.Inventory.Application.ReadModel.Ports;

public interface IReadModelStorage
{
    List<InventoryProjection> Inventories { get; }
}