namespace Domain;

public abstract class AggregateRoot
{
    public Guid Id { get; private set; }
    
    protected AggregateRoot(Guid aggregateId)
    {
        Id = aggregateId;
    }

    
}