using System;
using System.Diagnostics.Contracts;

namespace RoverConsole
{
    public enum TurnDirection
    {
        Left,
        Right
    }

    public abstract class Direction
    {
        public static implicit operator Direction(char direction)
        {
            switch (direction)
            {
                case 'N':
                    return new North();
                case 'S':
                    return new South();
                case 'E':
                    return new East();
                case 'W':
                    return new West();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        [Pure]
        public abstract Direction Rotate(TurnDirection turnDirection);
        public abstract (int x, int y) Move(int x, int y);
    }

    public class North : Direction
    {
        public override Direction Rotate(TurnDirection turnDirection)
        {
            switch (turnDirection)
            {
                case TurnDirection.Left:
                    return new West();
                case TurnDirection.Right:
                    return new East();
                default:
                    throw new ArgumentOutOfRangeException(nameof(turnDirection), turnDirection, null);
            }
        }

        public override (int x, int y) Move(int x, int y)
        {
            return (x, ++y);
        }
    }

    public class South : Direction
    {
        public override Direction Rotate(TurnDirection turnDirection)
        {
            switch (turnDirection)
            {
                case TurnDirection.Left:
                    return new East();
                case TurnDirection.Right:
                    return new West();
                default:
                    throw new ArgumentOutOfRangeException(nameof(turnDirection), turnDirection, null);
            }
        }
        public override (int x, int y) Move(int x, int y)
        {
            return (x, --y);
        }
    }

    public class East : Direction
    {
        public override Direction Rotate(TurnDirection turnDirection)
        {
            switch (turnDirection)
            {
                case TurnDirection.Left:
                    return new North();
                case TurnDirection.Right:
                    return new South();
                default:
                    throw new ArgumentOutOfRangeException(nameof(turnDirection), turnDirection, null);
            }
        }
        public override (int x, int y) Move(int x, int y)
        {
            return (++x, y);
        }
    }

    public class West : Direction
    {
        public override Direction Rotate(TurnDirection turnDirection)
        {
            switch (turnDirection)
            {
                case TurnDirection.Left:
                    return new South();
                case TurnDirection.Right:
                    return new North();
                default:
                    throw new ArgumentOutOfRangeException(nameof(turnDirection), turnDirection, null);
            }
        }
        public override (int x, int y) Move(int x, int y)
        {
            return (--x, y);
        }
    }
}
