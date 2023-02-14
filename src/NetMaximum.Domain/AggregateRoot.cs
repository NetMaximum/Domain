using System.Diagnostics.CodeAnalysis;

namespace NetMaximum.Domain;

public abstract class AggregateRoot
{
    public string Id { get; protected set; }

    [ExcludeFromCodeCoverage]
    protected AggregateRoot()
    {
    }
    
    protected AggregateRoot(string aggregateId)
    {
        Id = aggregateId;
    }
    
}