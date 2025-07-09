namespace EventSource.Inventory.Contracts.Abstractions;

public interface IDispatcher : IDispatchDispatcher
{
    void RegisterHandler<T>(Action<T> handler)
        where T : IMessage;
}

public interface IDispatchDispatcher
{
    void Dispatch(IEnumerable<IMessage> events);
}