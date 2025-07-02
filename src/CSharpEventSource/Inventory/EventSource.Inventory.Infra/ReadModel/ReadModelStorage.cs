using EventSource.Inventory.Application.Ports;
using EventSource.Inventory.Domain.ReadModel;

namespace EventSource.Inventory.Infra.ReadModel;

public class ReadModelStorage : IReadModelStorage
{
    public List<InventoryProjection> All { get; } = [];
}