using System.Reflection;

namespace NetMaximum.Domain.EventSourced;

public abstract class EventSourcedRepository
{
    private readonly IEventSourcedAggregateStore _store;

    protected EventSourcedRepository(IEventSourcedAggregateStore store)
    {
        _store = store;
    }
    
    public virtual Task<bool> ExistsAsync<T>(AggregateId<T> aggregateId) where T : EventSourcedAggregateRoot
    {
        return _store.ExistsAsync(aggregateId);
    }

    public Task SaveAsync<T>(T aggregate) where T : EventSourcedAggregateRoot
    {
        return _store.SaveAsync(aggregate);
    }

    public virtual async Task<Optional<T>> LoadAsync<T>(AggregateId<T> aggregateId) where T : EventSourcedAggregateRoot
    {
        
        var results = await _store.LoadAsync(aggregateId);
        if (!results.HasValue) return Optional<T>.None;
        
        var c= typeof(T).GetConstructor(
            BindingFlags.NonPublic |
            BindingFlags.Public |
            BindingFlags.Instance,
            null, 
            new[] { typeof(Guid) }, 
            null
        );
        
        var aggregate = (T)c.Invoke(new object[] {aggregateId.Value});
        aggregate.Load(results.Value!);
        return Optional<T>.Some(aggregate);
    }

    public virtual Task DeleteAsync<T>(AggregateId<T> aggregateId) where T : EventSourcedAggregateRoot
    {
        return _store.DeleteAsync(aggregateId);
    }
}