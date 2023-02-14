using System;
using NetMaximum.Domain.UnitTests.Examples;
using FluentAssertions;
using Xunit;

namespace NetMaximum.Domain.UnitTests;

public class AggregateIdTests
{
    [Fact]
    public void An_id_must_be_supplied()
    {
        // Arrange - Act
        var sut = new Action(() =>
        {
            var _ = new SampleAggregateRootId(string.Empty);
        });
        
        // Assert
        sut.Should().ThrowExactly<ArgumentNullException>().WithMessage("The Id cannot be empty (Parameter 'value')");
    }
    
    [Fact]
    public void Id_is_correct()
    {
        // Arrange - Act
        var id = Guid.NewGuid().ToString();
        var subject = new SampleAggregateRootId(id);

        // Assert
        subject.Value.Should().Be(id);
    }
    
    // [Fact]
    // public void Implicitly_converts_to_guid()
    // {
    //     // Arrange 
    //     var id = Guid.NewGuid().ToString();
    //     var subject = new SampleAggregateRootId(id);
    //
    //     // Act
    //     Guid result = subject;
    //
    //     // Assert
    //     result.Should().Be(id);
    // }

    [Fact]
    public void ToString_correctly_implemented_to_return_id_as_string()
    {
        // Arrange 
        var id = Guid.NewGuid().ToString();
        var subject = new SampleAggregateRootId(id);

        // Act
        var result = subject.ToString(); 

        // Assert
        result.Should().Be(id.ToString());
    }
    
}