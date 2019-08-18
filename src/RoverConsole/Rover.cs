using RoverConsole.Exceptions;

using System;
using System.Linq;

namespace RoverConsole
{
    public sealed class Rover
    {
        private readonly Mars _map;

        public Direction Orientation { get; private set; }

        public (int x, int y) Position { get; private set; }

        public Rover(Mars map)
        {
            _map = map;
        }

        public void Land(int x, int y, Direction orientation)
        {
            Orientation = orientation;
            SetPosition(x, y);

            _map.Rovers.Add(this);
        }

        public void Move()
        {
            var (newX, newY) = Orientation.Move(Position.x, Position.y);
            SetPosition(newX, newY);
        }

        public void Turn(TurnDirection turnDirection)
        {
            Orientation = Orientation.Rotate(turnDirection);
        }

        private void SetPosition(int x, int y)
        {
            var landingTerrain = _map.GetTerrain(x, y);
            switch (landingTerrain)
            {
                case Terrain.Plateau:
                    if (_map.Rovers.Any(r => r.Position == (x, y)))
                    {
                        throw new CollisionException();
                    }
                    Position = (x, y);
                    break;
                case Terrain.Unknown:
                    throw new OutOfPlateauException();

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
