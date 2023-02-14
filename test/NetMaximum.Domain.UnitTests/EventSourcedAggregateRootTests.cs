using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NetMaximum.Domain.UnitTests.Example;
using NetMaximum.Domain.UnitTests.Examples;
using Xunit;

namespace NetMaximum.Domain.UnitTests;

public class EventSourcedAggregateRootTests
{
    [Fact]
    public void Can_create_event_sourced_aggregate_root()
    {
        // Arrange - Act
        var id = Guid.NewGuid().ToString();
        var sut = new CustomerAggregateRoot(new SampleAggregateRootId(id), Name.FromString("Test","Name"));
        
        // // Assert
        sut.Should().NotBeNull();
        sut.Id.Should().Be(id);
    }
    
    [Fact]
    public void Can_create_event_sourced_aggregate_root_with_events()
    {
        // Arrange - Act
        var id = Guid.NewGuid().ToString();
        var sut = CreateSut(id);
        
        // // Assert
        sut.Should().NotBeNull();
        sut.Id.Should().Be(id);
        sut.GetEvents().Count().Should().Be(1);
    }
    
    [Fact]
    public void Clearing_events_should_also_reset_the_version()
    {
        // Arrange
        var id = Guid.NewGuid().ToString();
        var sut = CreateSut(id);
        
        sut.Should().NotBeNull();
        sut.Id.Should().Be(id);
        sut.GetEvents().Count().Should().Be(1);
        
        // Act
        sut.ClearEvents();
        
        // Assert
        sut.Version.Should().Be(0);
        sut.LoadedVersion.Should().Be(0);
        sut.GetEvents().Should().BeEmpty();
    }

    [Fact]
    public void After_a_series_of_events_has_been_loaded_calling_reset_should_also_clear_the_loaded_version()
    {
        // Arrange
        var id = Guid.NewGuid().ToString();
        var sut = CreateSut(id);
        
        sut.Load(new List<object>() {new object()});
        
        sut.Should().NotBeNull();
        sut.Id.Should().Be(id);
        sut.GetEvents().Count().Should().Be(2);
        sut.LoadedVersion.Should().Be(2);
        
        // Act 
        sut.ClearEvents();
        
        // Assert
        sut.LoadedVersion.Should().Be(0);
        sut.Version.Should().Be(0);
    }
    
    [Fact]
    public void Loading_a_series_of_events_should_store_the_version_the_aggregate_was_loaded_at()
    {
        // Arrange
        var id = Guid.NewGuid().ToString();
        var sut = CreateSut(id);
        
        sut.Load(new List<object>() {new object()});
        
        // Assert
        sut.Should().NotBeNull();
        sut.Id.Should().Be(id);
        sut.GetEvents().Count().Should().Be(2);
        sut.LoadedVersion.Should().Be(2);
    }

    [Fact]
    public void Applying_a_name_update_is_correctly_applied_to_the_aggregate()
    {
        // Arrange
        var id = Guid.NewGuid().ToString();
        var sut = CreateSut(id);
        
        // Act
        sut.UpdateName(Name.FromString("Chriss", "Barnard"));
        
        // Assert
        sut.Name.HasValue.Should().BeTrue();
        sut.Name.Value!.FirstName.Should().Be("Chriss");
        sut.Name.Value!.Surname.Should().Be("Barnard");
    }

    private CustomerAggregateRoot CreateSut(string id)
    {
        return new CustomerAggregateRoot(new SampleAggregateRootId(id), Name.FromString("Test","Name"));
    }
}