using System;
using NetMaximum.Domain.UnitTests.Example;

namespace NetMaximum.Domain.UnitTests.Examples;

public class SampleAggregateRootId : AggregateId<CustomerAggregateRoot>
{
    public SampleAggregateRootId(string value) : base(value)
    {
    }
}