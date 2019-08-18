using RoverConsole.Exceptions;

using System;

namespace RoverConsole
{
    public class Program
    {
        public static void Main()
        {
            var (height, length) = GetPlateauSize();

            var map = new Mars(height, length);

            while (true)
            {
                var rover = new Rover(map);
                SetupRoverLanding(rover);

                Console.WriteLine("Enter the commands for the rover: ");
                var roverCommands = ReadInput().ToCharArray();

                foreach (char command in roverCommands)
                {
                    if (!ExecuteCommand(rover, command))
                        break;
                }

                Console.WriteLine(
                    $"Rover now at Lat: {rover.Position.x} Long: {rover.Position.y} Orientation: {nameof(rover.Orientation)}");
            }
        }

        private static void SetupRoverLanding(Rover rover)
        {
            var (lat, longitude, orientation) = ReadLandingInformation();

            try
            {
                rover.Land(lat, longitude, orientation);
            }
            catch (OutOfPlateauException)
            {
                Console.WriteLine("Unable to complete command, position out of plateau limits.");
                SetupRoverLanding(rover);
            }
            catch (CollisionException)
            {
                Console.WriteLine("Unable to complete command, position occupied by another rover.");
                SetupRoverLanding(rover);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                SetupRoverLanding(rover);
            }
        }

        private static bool ExecuteCommand(Rover rover, char command)
        {
            switch (command)
            {
                case 'L':
                    rover.Turn(TurnDirection.Left);
                    return true;
                case 'R':
                    rover.Turn(TurnDirection.Right);
                    return true;
                case 'M':
                    try
                    {
                        rover.Move();
                        return true;
                    }
                    catch (OutOfPlateauException)
                    {
                        Console.WriteLine("Unable to complete command, position out of plateau limits.");
                        return false;
                    }
                    catch (CollisionException)
                    {
                        Console.WriteLine("Unable to complete command, position occupied by another rover.");
                        return false;
                    }
                default:
                    Console.WriteLine($"Unrecognized command '${command}' found, skipping...");
                    return false;
            }
        }

        private static (int lat, int longitude, char orientation) ReadLandingInformation()
        {
            Console.WriteLine("Enter Rover launch information: ");
            var roverInitialInfo = ReadInput();

            var roverLandingInfo = roverInitialInfo.Split(' ');

            if (roverLandingInfo.Length == 3)
            {
                if (int.TryParse(roverLandingInfo[0], out var latitude)
                    && int.TryParse(roverLandingInfo[1], out var longitude)
                    && char.TryParse(roverLandingInfo[2], out var orientation))
                {
                    return (latitude, longitude, orientation);
                }
            }
            Console.WriteLine("Invalid input.");
            return ReadLandingInformation();
        }

        private static (int heigth, int length) GetPlateauSize()
        {
            Console.WriteLine("Enter the size of the plateau:");

            var plateauSizeInput = ReadInput();

            var plateauSize = plateauSizeInput.Split(' ');

            if (plateauSize.Length == 2)
            {
                if (int.TryParse(plateauSize[0], out var height)
                    && int.TryParse(plateauSize[1], out var length))
                {
                    return (height, length);
                }
            }
            Console.WriteLine("Invalid input.");
            return GetPlateauSize();
        }

        private static string ReadInput()
        {
            string input;
            do
            {
                input = Console.ReadLine();
            } while (string.IsNullOrEmpty(input));

            return input.ToUpper();
        }
    }
}
