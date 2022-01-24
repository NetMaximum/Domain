using System;

namespace NetMaximum.Domain.UnitTests.Examples;

public class SampleAggregateRootId : AggregateId<SampleAggregateRoot>
{
    public SampleAggregateRootId(Guid value) : base(value)
    {
    }
}