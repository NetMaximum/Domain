using System;
using Domain.UnitTests.Examples;
using FluentAssertions;
using Xunit;

namespace Domain.UnitTests;

public class AggregateRootTests
{
    [Fact]
    public void Default_id_is_correct()
    {
        // Arrange
        var id = Guid.NewGuid();
        var sut = new SampleAggregateRoot(new SampleAggregateRootId(id));
        
        // Assert
        sut.Id.Should().Be(id);
    }
}