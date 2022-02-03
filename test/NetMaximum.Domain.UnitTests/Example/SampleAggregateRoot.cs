namespace NetMaximum.Domain.UnitTests.Examples;

public class SampleAggregateRoot : AggregateRoot
{
    private readonly SampleAggregateRootId _id;

    public SampleAggregateRoot(SampleAggregateRootId id) : base(id)
    {
        _id = id;
    }    
}