namespace NetMaximum.Domain
{
    public interface IEventSourcedAggregateStore
    {
        Task<bool> ExistsAsync<T>(AggregateId<T> aggregateId) where T : EventSourcedAggregateRoot<T>;
    
        Task SaveAsync<T>(T aggregate) where T : EventSourcedAggregateRoot<T>;
    
        Task<Optional<T>> LoadAsync<T>(AggregateId<T> aggregateId) where T : EventSourcedAggregateRoot<T>;
    
        Task DeleteAsync<T>(AggregateId<T> aggregateId) where T : EventSourcedAggregateRoot<T>;
    }
}