using EventSource.Inventory.Application.ReadModel;
using EventSource.Inventory.Infra.ReadModel;
using EventSource.Inventory.Infra.Tools;

namespace SampleESConsoleApp.Helpers;

public static class ReadModelHelper
{
    public static ReadModelMaster CreateReadModel(DomainEventDispatcher domainEventDispatcher)
    {
        var readStorage = new ReadModelStorage();
        var master = new ReadModelMaster(readStorage);
        master.Init(domainEventDispatcher);

        return master;
    }
}