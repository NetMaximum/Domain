using System;
using FluentAssertions;
using NetMaximum.Domain.UnitTests.Examples;
using Xunit;

namespace NetMaximum.Domain.UnitTests;

public class AggregateRootTests
{
    [Fact]
    public void Default_id_is_correct()
    {
        // Arrange
        var id = Guid.NewGuid().ToString();
        var sut = new SampleAggregateRoot(new SampleAggregateRootId(id));
        
        // Assert
        sut.Id.Should().Be(id);
    }
}