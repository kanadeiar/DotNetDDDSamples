using ESCQRS.Inventory.Application.ReadModel;
using ESCQRS.Inventory.Infra.ReadModel.Adapters;
using ESCQRS.Inventory.Infra.Tools;

namespace ESCQRSConsoleApp.Helpers;

public static class ReadModelHelper
{
    public static ReadModelMaster CreateMaster(DomainEventDispatcher domainEventDispatcher)
    {
        var readStorage = new ReadModelStorage();
        var master = new ReadModelMaster(readStorage);
        master.Init(domainEventDispatcher);

        return master;
    }
}