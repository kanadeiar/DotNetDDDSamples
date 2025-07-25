﻿namespace DomainModel.Inventory.Contracts.Base;

public class Aggregate
{
    private readonly List<DomainEvent> _events = new();

    protected void AddEvent(DomainEvent @event)
    {
        _events.Add(@event);
    }

    public IEnumerable<DomainEvent> TakeEvents()
    {
        var results = _events.ToList();
        _events.Clear();

        return results;
    }
}