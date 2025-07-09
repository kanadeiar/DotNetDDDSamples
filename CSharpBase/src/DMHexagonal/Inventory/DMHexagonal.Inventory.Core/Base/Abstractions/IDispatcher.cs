namespace DMHexagonal.Inventory.Core.Base.Abstractions;

public interface IDispatcher : IRegisterDispatcher, IDispatchDispatcher;

public interface IRegisterDispatcher
{
    void RegisterHandler<T>(Action<T> handler)
        where T : IMessage;
}

public interface IDispatchDispatcher
{
    public void Dispatch(IEnumerable<IMessage> events);
}