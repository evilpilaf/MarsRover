# MARS ROVER

## DESCRIPTION

I've built a C# console application for resolving the Mars Rover excersie, this solution is split in four main class files:

* Direction.cs: Holds all logic regarding the changing of directions, what turning in a certain orientation means and what direction this change results in. It also aids in calculating the resulting position of the rover given a starting point

* Mars.cs: Contains the plateau information, with the size given by the space flight experts, it helps in knowing what kind of terrain is in any given possition and to observe where any rover is.

* Rover.cs: Exposes the commands available for the command crew in a rover, this way the crew can move and reorient a rover as desired. The rover will inspect the map at every _MOVE_ command to ensure that it's still within the plateau and to ensure it will not crash with another rover.

* Program.cs: Entry point of the application, it does the parsing of all commands entered by the crew and transfers them to the rover, if an error happens the console will display it. If an invalid command is given or the command can result in the damage of equipment the operator will be informed and the command won't be executed

## ASSUMPTIONS

* There can be an unlimitted number of rovers in a map

* Instructions are given and executed one rover at a time

* All move instructions for a rover are given within a single line.

* After commands are given for a rover, the following input will launch a new one

* Two rovers cannot occupy the same space
