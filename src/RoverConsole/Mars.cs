using System.Collections.Generic;

namespace RoverConsole
{
    public sealed class Mars
    {
        public List<Rover> Rovers;
        public (int height, int lenght) Plateau { get; }

        public Mars(int height, int length)
        {
            Plateau = (height, length);
            Rovers = new List<Rover>();
        }

        public Terrain GetTerrain(int x, int y)
        {
            if (x >= 0 &&
                y >= 0 &&
                y <= Plateau.height &&
                x <= Plateau.lenght)
                return Terrain.Plateau;

            return Terrain.Unknown;
        }
    }

    public enum Terrain
    {
        Plateau,
        Unknown
    }
}
