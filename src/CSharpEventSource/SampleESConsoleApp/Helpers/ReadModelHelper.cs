using EventSource.Inventory.Application.ReadModel;
using EventSource.Inventory.Infra.ReadModel;
using EventSource.Inventory.Infra.Tools;

namespace SampleESConsoleApp.Helpers;

public static class ReadModelHelper
{
    public static Master CreateReadModel(DomainEventDispatcher domainEventDispatcher)
    {
        var readStorage = new ReadModelStorage();
        var master = new Master(readStorage);
        master.Init(domainEventDispatcher);

        return master;
    }
}