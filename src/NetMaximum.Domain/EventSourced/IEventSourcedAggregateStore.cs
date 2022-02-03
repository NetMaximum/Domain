namespace NetMaximum.Domain.EventSourced
{
    public interface IEventSourcedAggregateStore
    {
        Task<bool> ExistsAsync<T>(AggregateId<T> aggregateId) where T : EventSourcedAggregateRoot;
    
        Task SaveAsync<T>(T aggregate) where T : EventSourcedAggregateRoot;
    
        Task<Optional<IEnumerable<object>>> LoadAsync<T>(AggregateId<T> aggregateId) where T : EventSourcedAggregateRoot;
    
        Task DeleteAsync<T>(AggregateId<T> aggregateId) where T : EventSourcedAggregateRoot;
    }
}