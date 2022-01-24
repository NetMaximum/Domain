namespace Domain.UnitTests.Examples;

public class SampleAggregateRoot : AggregateRoot
{
    private readonly AggregateId<SampleAggregateRoot> _id;

    public SampleAggregateRoot(AggregateId<SampleAggregateRoot> id) : base(id)
    {
        _id = id;
    }    
}