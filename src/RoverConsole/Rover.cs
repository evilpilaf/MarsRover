using RoverConsole.Exceptions;

using System;
using System.Linq;

namespace RoverConsole
{
    public sealed class Rover
    {
        private readonly Mars _map;

        private Direction _orientation;

        public (int x, int y) Position;

        public Rover(Mars map)
        {
            _map = map;
        }

        public void Land(int x, int y, Direction orientation)
        {
            _orientation = orientation;
            SetPosition(x, y);

            _map.Rovers.Add(this);
        }

        public void Move()
        {
            var (newX, newY) = _orientation.Move(Position.x, Position.y);
            SetPosition(newX, newY);
        }

        public void Reorient(Direction orientation)
        {
            _orientation = orientation;
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
