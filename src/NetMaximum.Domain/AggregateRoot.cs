namespace NetMaximum.Domain;

public abstract class AggregateRoot
{
    public Guid Id { get; protected set; }

    protected AggregateRoot()
    {
        
    }
    
    protected AggregateRoot(Guid aggregateId)
    {
        Id = aggregateId;
    }
    
}