using FluentAssertions;

using RoverConsole.Exceptions;

using System;
using System.Linq;

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

        [Fact]
        public void Land_WhenSuccess_MarksSelfInMap()
        {
            const int plateauSize = 2;

            var map = new Mars(plateauSize, plateauSize);

            var sut = new Rover(map);

            sut.Land(0, 0, 'N');

            map.Rovers.Should().HaveCount(1);
            map.Rovers.Single().Should().Be(sut);
        }

        [Fact]
        public void Move_WhenResultsInFallFromPlateau_ThrowsOutOfPlateauException()
        {
            const int plateauSize = 1;
            var map = new Mars(plateauSize, plateauSize);

            var sut = CreateSut(map);

            sut.Land(plateauSize, plateauSize, 'N');

            Action action = () => sut.Move();

            action.Should().ThrowExactly<OutOfPlateauException>();
        }

        [Fact]
        public void Land_WhenActionResultsInClashWithOtherRover_ThrowsCollisionException_DoesNotMoveToSpace()
        {
            const int plateauSize = 1;
            var map = new Mars(plateauSize, plateauSize);

            var other = CreateSut(map);
            var sut = CreateSut(map);

            other.Land(plateauSize, plateauSize, 'N');

            Action action = () => sut.Land(plateauSize, plateauSize, 'N');

            action.Should().ThrowExactly<CollisionException>();
        }

        [Fact]
        public void Move_WhenActionResultsInClashWithOtherRover_ThrowsCollisionException_DoesNotMoveToSpace()
        {
            const int plateauSize = 1;
            var map = new Mars(plateauSize, plateauSize);

            var other = CreateSut(map);
            var sut = CreateSut(map);

            other.Land(plateauSize, plateauSize, 'N');

            sut.Land(plateauSize, plateauSize - 1, 'N');

            Action action = () => sut.Move();

            action.Should().ThrowExactly<CollisionException>();
        }

        [Theory]
        [InlineData('N', typeof(East))]
        [InlineData('E', typeof(South))]
        [InlineData('S', typeof(West))]
        [InlineData('W', typeof(North))]
        public void TurnRight_WithValidArgument_ResultsInRoverFacingCorrectDirection(char landingOrientation, Type expectedOutcome)
        {
            const int plateauSize = 2;

            var map = new Mars(plateauSize, plateauSize);

            var sut = new Rover(map);

            sut.Land(0, 0, landingOrientation);
            sut.Turn(TurnDirection.Right);

            sut.Orientation.Should().BeOfType(expectedOutcome);
        }

        [Theory]
        [InlineData('N', typeof(West))]
        [InlineData('W', typeof(South))]
        [InlineData('S', typeof(East))]
        [InlineData('E', typeof(North))]
        public void TurnLeft_WithValidArgument_ResultsInRoverFacingCorrectDirection(char landingOrientation, Type expectedOutcome)
        {
            const int plateauSize = 2;

            var map = new Mars(plateauSize, plateauSize);

            var sut = new Rover(map);

            sut.Land(0, 0, landingOrientation);
            sut.Turn(TurnDirection.Left);

            sut.Orientation.Should().BeOfType(expectedOutcome);
        }

        [Fact]
        public void Move_WhenPathIsClear_AndInPlateau_ResultsInRoverInNewPosition()
        {
            const int initialX = 0;
            const int initialY = 0;
            const int plateauSize = 2;

            var map = new Mars(plateauSize, plateauSize);

            var sut = new Rover(map);

            sut.Land(initialX, initialY, 'N');
            sut.Move();

            sut.Position.x.Should().Be(initialX);
            sut.Position.y.Should().Be(initialY + 1);
        }

        private Rover CreateSut(Mars map)
        {
            return new Rover(map);
        }
    }
}
