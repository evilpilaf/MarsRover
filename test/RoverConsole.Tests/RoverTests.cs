using FluentAssertions;

using RoverConsole.Exceptions;

using System;

using Xunit;

namespace RoverConsole.Tests
{
    public class RoverTests
    {
        [Fact]
        public void Landing_WhenTryingInInvalidTerrain_ThrowsOutOfPlateauException()
        {
            const int plateauSize = 2;

            var map = new Mars(plateauSize, plateauSize);

            var sut = new Rover(map);

            Action action = () => sut.Land(plateauSize + 1, plateauSize + 1, 'S');

            action.Should().ThrowExactly<OutOfPlateauException>();
        }
    }
}
