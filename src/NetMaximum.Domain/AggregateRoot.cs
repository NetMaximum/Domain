using System.Diagnostics.CodeAnalysis;

namespace NetMaximum.Domain;

public abstract class AggregateRoot
{
    public Guid Id { get; protected set; }

    [ExcludeFromCodeCoverage]
    protected AggregateRoot()
    {
    }
    
    protected AggregateRoot(Guid aggregateId)
    {
        Id = aggregateId;
    }
    
}