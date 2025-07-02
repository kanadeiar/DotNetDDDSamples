using EventSource.Inventory.Domain.ReadModel;

namespace EventSource.Inventory.Application.Ports;

public interface IReadModelStorage
{
    List<InventoryProjection> All { get; }
}