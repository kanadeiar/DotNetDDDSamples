namespace DMHex.Inventory.Core.Base.Abstractions;

public interface IDispatcher : IDispatchDispatcher
{
    void RegisterHandler<T>(Action<T> handler)
        where T : IMessage;
}

public interface IDispatchDispatcher
{
    public void Dispatch(IEnumerable<IMessage> events);
}