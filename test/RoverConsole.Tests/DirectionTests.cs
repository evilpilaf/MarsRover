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
