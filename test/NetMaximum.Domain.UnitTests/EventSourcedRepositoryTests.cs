using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using NetMaximum.Domain.EventSourced;
using NetMaximum.Domain.UnitTests.Example;
using NetMaximum.Domain.UnitTests.Example.Events;
using NetMaximum.Domain.UnitTests.Examples;
using Xunit;

namespace NetMaximum.Domain.UnitTests;

public class EventSourcedRepositoryTests
{
    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public async Task Aggregate_exists(bool exists)
    {
        // Arrange
        var id = new SampleAggregateRootId(Guid.NewGuid().ToString());
        var aggStore = new Mock<IEventSourcedAggregateStore>();
        aggStore.Setup(x => x.ExistsAsync(id)).ReturnsAsync(exists);
        
        var sut = new FakeCustomerAggregateStore(aggStore.Object);
        
        // Act
        var result = await sut.ExistsAsync(id);
        
        // Assert
        aggStore.Verify(x => x.ExistsAsync(id), Times.Once);
        result.Should().Be(exists);
    }

    [Fact]
    public async Task When_an_aggregate_load_happens_on_unknown_id_an_optional_of_none_is_returned()
    {
        // Arrange
        var id = new SampleAggregateRootId(Guid.NewGuid().ToString());
        var aggStore = new Mock<IEventSourcedAggregateStore>();
        aggStore.Setup(x => x.LoadAsync(id)).ReturnsAsync(Optional<IEnumerable<object>>.None);
        
        var sut = new FakeCustomerAggregateStore(aggStore.Object);
        
        // Act
        var result = await sut.LoadAsync(id);
        
        // Assert
        result.HasValue.Should().BeFalse();
        aggStore.Verify(x => x.LoadAsync(id), Times.Once);
    }
    
    [Fact]
    public async Task When_an_aggregate_has_a_non_public_ctor_its_correctly_created()
    {
        // Arrange
        var id = new SampleAggregateRootId(Guid.NewGuid().ToString());
        var aggStore = new Mock<IEventSourcedAggregateStore>();
        aggStore.Setup(x => x.LoadAsync(id)).ReturnsAsync(Optional<IEnumerable<object>>.Some(new List<object>() {new CustomerCreated(Name.FromString("Test", "Surname"))}));
        
        var sut = new FakeCustomerAggregateStore(aggStore.Object);
        
        // Act
        var result = await sut.LoadAsync(id);
        
        // Assert
        result.HasValue.Should().BeTrue();
        aggStore.Verify(x => x.LoadAsync(id), Times.Once);
        result.Value!.LoadedVersion.Should().Be(1);
        result.Value!.Version.Should().Be(1);
    }
    
    [Fact]
    public async Task When_an_aggregate_has_a_public_ctor_its_correctly_created()
    {
        // Arrange
        var id = new PublicCTORAggregateRootId(Guid.NewGuid().ToString());
        var aggStore = new Mock<IEventSourcedAggregateStore>();
        aggStore.Setup(x => x.LoadAsync(id)).ReturnsAsync(Optional<IEnumerable<object>>.Some(new List<object>() {new CustomerCreated(Name.FromString("Test", "Surname"))}));
        
        var sut = new FakePublicCtorAggregateStore(aggStore.Object);
        
        // Act
        var result = await sut.LoadAsync(id);
        
        // Assert
        result.HasValue.Should().BeTrue();
        aggStore.Verify(x => x.LoadAsync(id), Times.Once);
        result.Value!.LoadedVersion.Should().Be(1);
        result.Value!.Version.Should().Be(1);
    }

    [Fact]
    public async Task An_aggregate_save_is_delegated_to_the_store_interface()
    {
        // Arrange
        var aggregate = new CustomerAggregateRoot(Guid.NewGuid().ToString(), Name.FromString("firstName", "surname"));
        
        var aggStore = new Mock<IEventSourcedAggregateStore>();
        var sut = new FakeCustomerAggregateStore(aggStore.Object);
        
        // Act
        await sut.SaveAsync(aggregate);

        // Assert
        aggStore.Verify(x => x.SaveAsync(aggregate), Times.Once);
    }
    
    [Fact]
    public async Task An_aggregate_delete_is_delegated_to_the_store_interface()
    {
        // Arrange
        var id = new SampleAggregateRootId(Guid.NewGuid().ToString());
        var aggStore = new Mock<IEventSourcedAggregateStore>();
        var sut = new FakeCustomerAggregateStore(aggStore.Object);
        // Act
        
        await sut.DeleteAsync(id);

        // Assert
        aggStore.Verify(x => x.DeleteAsync(id), Times.Once);
    }
    
    private class FakePublicCtorAggregateStore : EventSourcedRepository<PublicCTORCustomerAggregateRoot>
    {
        public FakePublicCtorAggregateStore(IEventSourcedAggregateStore store) : base(store)
        {
        }
    }
    
    private class FakeCustomerAggregateStore : EventSourcedRepository<CustomerAggregateRoot>
    {
        public FakeCustomerAggregateStore(IEventSourcedAggregateStore store) : base(store)
        {
        }
    }
}