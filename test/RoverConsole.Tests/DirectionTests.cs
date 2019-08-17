using FluentAssertions;

using System;
using System.Collections.Generic;
using System.Linq;

using Xunit;

namespace RoverConsole.Tests
{
    public class DirectionTests
    {
        [Theory]
        [InlineData('N', typeof(North))]
        [InlineData('S', typeof(South))]
        [InlineData('E', typeof(East))]
        [InlineData('W', typeof(West))]
        public void Creation_WhenUsingValidDirection_ReturnsCorrectType(char direction, Type type)
        {
            Direction sut = direction;

            sut.Should().BeOfType(type);
        }

        [Theory]
        [MemberData(nameof(GetInvalidChar))]
        public void Creation_WhenUsingInvalidDirection_ThrowsArgumentOutOfRangeException(char invalidValue)
        {
            Direction sut;

            Action action = () => sut = invalidValue;

            action.Should().ThrowExactly<ArgumentOutOfRangeException>();
        }

        [Theory]
        [InlineData('N', typeof(West))]
        [InlineData('W', typeof(South))]
        [InlineData('S', typeof(East))]
        [InlineData('E', typeof(North))]
        public void RotateLeft_FromStarterDirection_ShouldResultInCorrect(char start, Type end)
        {
            Direction sut = start;

            var result = sut.Rotate(TurnDirection.Left);

            result.Should().BeOfType(end);
        }

        [Theory]
        [InlineData('N', typeof(East))]
        [InlineData('E', typeof(South))]
        [InlineData('S', typeof(West))]
        [InlineData('W', typeof(North))]
        public void RotateRight_FromStarterDirection_ShouldResultInCorrect(char start, Type end)
        {
            Direction sut = start;

            var result = sut.Rotate(TurnDirection.Right);

            result.Should().BeOfType(end);
        }

        [Fact]
        public void Move_HeadedNorth_ResultsInHigherPosition()
        {
            const int startX = 10;
            const int startY = 12;

            Direction sut = new North();

            var moveResult = sut.Move(startX, startY);

            moveResult.x.Should().Be(startX);
            moveResult.y.Should().Be(startY + 1);
        }

        [Fact]
        public void Move_HeadedEast_ResultsInPositionOneToTheRight()
        {
            const int startX = 10;
            const int startY = 12;

            Direction sut = new East();

            var (newX, newY) = sut.Move(startX, startY);

            newX.Should().Be(startX + 1);
            newY.Should().Be(startY);
        }

        [Fact]
        public void Move_HeadedSouth_ResultsInPositionOneDown()
        {
            const int startX = 10;
            const int startY = 12;

            Direction sut = new South();

            var moveResult = sut.Move(startX, startY);

            moveResult.x.Should().Be(startX);
            moveResult.y.Should().Be(startY - 1);
        }

        [Fact]
        public void Move_HeadedWest_ResultsInPositionToTheLeft()
        {
            const int startX = 10;
            const int startY = 12;

            Direction sut = new West();

            var moveResult = sut.Move(startX, startY);

            moveResult.x.Should().Be(startX - 1);
            moveResult.y.Should().Be(startY);
        }

        public static IEnumerable<object[]> GetInvalidChar()
        {
            var allowed = new[] { 'N', 'S', 'E', 'W' };
            const string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            char[] alphas = (alphabet + alphabet.ToLower()).ToCharArray();

            foreach (var a in alphas)
            {
                if (!allowed.Contains(a))
                    yield return new object[] { a };
            }
        }
    }
}
