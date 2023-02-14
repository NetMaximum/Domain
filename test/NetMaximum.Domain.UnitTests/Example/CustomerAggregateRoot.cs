using System;
using NetMaximum.Domain.EventSourced;
using NetMaximum.Domain.UnitTests.Example.Events;
using NetMaximum.Domain.UnitTests.Examples;

namespace NetMaximum.Domain.UnitTests.Example;

public class CustomerAggregateRoot : EventSourcedAggregateRoot
{
    public Optional<Name> Name { get; private set; } = Optional<Name>.None;

    public CustomerAggregateRoot (string id, Name name) : base(id)
    {
        Apply(new CustomerCreated(name));
    }

    protected CustomerAggregateRoot(string id) : base(id)
    {
        
    }
    
    public void UpdateName(Name name)
    {
        Apply(new NameUpdated(name));
    }
    
    protected override void When(object @event)
    {
        switch (@event)
        {
            case CustomerCreated customerCreated:
                Name = Optional<Name>.Some(customerCreated.Name);
                break;
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
}