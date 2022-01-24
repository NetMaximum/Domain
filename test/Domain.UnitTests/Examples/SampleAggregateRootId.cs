using System;

namespace Domain.UnitTests.Examples;

public class SampleAggregateRootId : AggregateId<SampleAggregateRoot>
{
    public SampleAggregateRootId(Guid value) : base(value)
    {
    }
}