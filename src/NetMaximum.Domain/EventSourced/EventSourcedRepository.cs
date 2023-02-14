using System.Reflection;
using System.Security.Cryptography.X509Certificates;

namespace NetMaximum.Domain.EventSourced;

public abstract class EventSourcedRepository<T> where T : EventSourcedAggregateRoot
{
    private readonly IEventSourcedAggregateStore _store;

    protected EventSourcedRepository(IEventSourcedAggregateStore store)
    {
        _store = store;
    }
    
    public virtual Task<bool> ExistsAsync(AggregateId<T> aggregateId) //where T : EventSourcedAggregateRoot
    {
        return _store.ExistsAsync(aggregateId);
    }

    public Task SaveAsync(T aggregate) //where T : EventSourcedAggregateRoot
    {
        return _store.SaveAsync(aggregate);
    }

    public virtual async Task<Optional<T>> LoadAsync(AggregateId<T> aggregateId) //where T : EventSourcedAggregateRoot
    {
        
        var results = await _store.LoadAsync(aggregateId);
        if (!results.HasValue) return Optional<T>.None;
        
        var c= typeof(T).GetConstructor(
            BindingFlags.NonPublic |
            BindingFlags.Public |
            BindingFlags.Instance,
            null, 
            new[] { typeof(string) }, 
            null
        );
        
        var aggregate = (T)c.Invoke(new object[] {aggregateId.Value});
        aggregate.Load(results.Value!);
        return Optional<T>.Some(aggregate);
    }

    public virtual Task DeleteAsync(AggregateId<T> aggregateId) //where T : EventSourcedAggregateRoot
    {
        return _store.DeleteAsync(aggregateId);
    }
}