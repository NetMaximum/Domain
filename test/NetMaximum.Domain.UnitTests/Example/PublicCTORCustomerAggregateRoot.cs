using System;
using NetMaximum.Domain.EventSourced;
using NetMaximum.Domain.UnitTests.Example.Events;
using NetMaximum.Domain.UnitTests.Examples;

namespace NetMaximum.Domain.UnitTests.Example;

public class PublicCTORCustomerAggregateRoot : EventSourcedAggregateRoot
{
    public PublicCTORCustomerAggregateRoot(string id) : base(id)
    {
    }
    
    protected override void When(object @event)
    {
    }

    protected override void EnsureValidState()
    {
    }
}