using System;
using System.Collections.Generic;
using NetMaximum.Domain.UnitTests.Example.Events;
using NetMaximum.Domain.UnitTests.Examples;

namespace NetMaximum.Domain.UnitTests.Example;

public class CustomerAggregateRoot : EventSourcedAggregateRoot<CustomerAggregateRoot>
{
    public Optional<Name> Name { get; private set; } = Optional<Name>.None;

    public void UpdateName(Name name)
    {
        Apply(new NameUpdated(name));
    }
    
    protected override void When(object @event)
    {
        switch (@event)
        {
            case NameUpdated nameUpdated:
                Name = Optional<Name>.Some(nameUpdated.Name);
                break;
        }
    }

    protected override void EnsureValidState()
    {
        if (!this.Name.HasValue)
        {
            throw new DomainException();
        }
    }

    public CustomerAggregateRoot(Guid aggregateId, object initialEvent)
    {
        Apply(initialEvent);
    }

    public static CustomerAggregateRoot Create(Guid id, Name name)
    {
        var aggregate =  new CustomerAggregateRoot(id);
        aggregate.Apply(new CustomerCreated(name));
        return aggregate;
    }
    
    // Todo : Add something to deal with the initial state - Customer Created Event.
    private CustomerAggregateRoot(Guid aggregateId) : base(aggregateId)
    {
    }

    private CustomerAggregateRoot(Guid aggregateId, IEnumerable<object> events) : base(aggregateId, events)
    {
    }
}