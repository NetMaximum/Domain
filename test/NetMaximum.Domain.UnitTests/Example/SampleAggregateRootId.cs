using System;
using NetMaximum.Domain.UnitTests.Example;

namespace NetMaximum.Domain.UnitTests.Examples;

public class SampleAggregateRootId : AggregateId<CustomerAggregateRoot>
{
    public SampleAggregateRootId(Guid value) : base(value)
    {
    }
}