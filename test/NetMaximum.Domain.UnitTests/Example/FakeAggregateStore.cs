using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetMaximum.Domain.UnitTests.Example;

public class FakeAggregateStore : IEventSourcedAggregateStore
{
    private static Dictionary<string, IEnumerable<object>> _store = new();

    public Task<bool> ExistsAsync<T>(AggregateId<T> aggregateId) where T : EventSourcedAggregateRoot<T>
    {
        return Task.FromResult(_store.ContainsKey(GetStreamName(aggregateId)));
    }

    public Task SaveAsync<T>(T aggregate) where T : EventSourcedAggregateRoot<T>
    {
        _store.Add(GetStreamName(aggregate), aggregate.GetEvents());
        return Task.CompletedTask;
    }

    public Task<Optional<T>> LoadAsync<T>(AggregateId<T> aggregateId) where T : EventSourcedAggregateRoot<T>
    {
        var aggregate = (T) Activator.CreateInstance(typeof(T), true);
        if (aggregate == null)
        {
            return Task.FromResult(Optional<T>.None);
        }
        
        aggregate.Load(aggregateId, new List<object>());
        return Task.FromResult(Optional<T>.Some(aggregate));
    }

    public Task DeleteAsync<T>(AggregateId<T> aggregateId) where T : EventSourcedAggregateRoot<T>
    {
        throw new System.NotImplementedException();
    }

    static string GetStreamName<T>(AggregateId<T> aggregateId)
        where T : AggregateRoot
        => $"{typeof(T).Name}-{aggregateId}";

    static string GetStreamName<T>(T aggregate)
        where T : AggregateRoot
        => $"{typeof(T).Name}-{aggregate.Id.ToString()}";
}