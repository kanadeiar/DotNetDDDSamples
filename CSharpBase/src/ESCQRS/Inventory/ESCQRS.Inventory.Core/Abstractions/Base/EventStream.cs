using ESCQRS.Inventory.Core.Abstractions.Base.Abstractions;

namespace ESCQRS.Inventory.Core.Abstractions.Base;

public record EventStream(ICollection<IMessage> Events, int Version = -1);
