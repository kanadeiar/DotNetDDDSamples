using ESCQRS.Inventory.Core.Base.Abstractions;

namespace ESCQRS.Inventory.Core.Base;

public record EventStream(ICollection<IMessage> Events, int Version = -1);
