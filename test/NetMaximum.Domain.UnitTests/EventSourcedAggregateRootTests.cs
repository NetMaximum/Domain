using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NetMaximum.Domain.UnitTests.Example;
using NetMaximum.Domain.UnitTests.Example.Events;
using NetMaximum.Domain.UnitTests.Examples;
using Xunit;

namespace NetMaximum.Domain.UnitTests;

public class EventSourcedAggregateRootTests
{
    [Fact]
    public void Can_create_event_sourced_aggregate_root()
    {
        // Arrange - Act
        var id = Guid.NewGuid();
        var sut = CustomerAggregateRoot.Create(new SampleAggregateRootId(id), Name.FromString("Test","Name"));
        // // Assert
        sut.Should().NotBeNull();
        sut.Id.Should().Be(id);
    }
    
    // [Fact]
    // public void Can_create_event_sourced_aggregate_root_with_events()
    // {
    //     // Arrange - Act
    //     var id = Guid.NewGuid();
    //     var sut = CustomerAggregateRoot.Create(new SampleAggregateRootId(id), new List<object> {new NameUpdated(Name.FromString("Chriss", "Barnard"))});
    //     
    //     // // Assert
    //     sut.Should().NotBeNull();
    //     sut.Id.Should().Be(id);
    //     sut.GetEvents().Count().Should().Be(1);
    // }
    //
    // [Fact]
    // public void Clearing_events_should_also_reset_the_version()
    // {
    //     // Arrange
    //     var id = Guid.NewGuid();
    //     var sut = CustomerAggregateRoot.Create(new SampleAggregateRootId(id), new List<object> {new NameUpdated(Name.FromString("Chriss", "Barnard"))});
    //     
    //     sut.Should().NotBeNull();
    //     sut.Id.Should().Be(id);
    //     sut.GetEvents().Count().Should().Be(1);
    //     
    //     // Act
    //     sut.ClearEvents();
    //     
    //     // Assert
    //     sut.Version.Should().Be(0);
    //     sut.GetEvents().Should().BeEmpty();
    //
    // }

    // [Fact]
    // public void Loading_a_series_of_events_should_store_the_version_the_aggregate_was_loaded_at()
    // {
    //     // Arrange
    //     var id = Guid.NewGuid();
    //     var sut = CustomerAggregateRoot.Create(new SampleAggregateRootId(id), new List<object> {new object()});
    //     
    //     // Assert
    //     sut.Should().NotBeNull();
    //     sut.Id.Should().Be(id);
    //     sut.GetEvents().Count().Should().Be(1);
    //     sut.LoadedVersion.Should().Be(1);
    // }

    [Fact]
    public void Applying_a_name_update_is_correctly_applied_to_the_aggregate()
    {
        // Arrange
        var id = Guid.NewGuid();
        var sut = CustomerAggregateRoot.Create(new SampleAggregateRootId(id), Name.FromString("Test","Name"));
        sut.Name.HasValue.Should().BeFalse();
        
        // Act
        sut.UpdateName(Name.FromString("Chriss", "Barnard"));
        
        // Assert
        sut.Name.HasValue.Should().BeTrue();
        sut.Name.Value.FirstName.Should().Be("Chriss");
        sut.Name.Value.Surname.Should().Be("Barnard");
    }
}