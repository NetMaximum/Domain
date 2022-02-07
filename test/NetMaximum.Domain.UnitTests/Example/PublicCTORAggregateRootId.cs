using System;
using NetMaximum.Domain.UnitTests.Example;

namespace NetMaximum.Domain.UnitTests.Examples;

public class PublicCTORAggregateRootId : AggregateId<PublicCTORCustomerAggregateRoot>
{
    public PublicCTORAggregateRootId(Guid value) : base(value)
    {
    }
}