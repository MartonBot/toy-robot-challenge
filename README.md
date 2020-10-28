# Martin's Toy Robot Challenge

I was asked to do the toy robot challenge for an interview. Here is my implementation.

## Getting Started

Clone the project, `cd` into the `ToyRobotChallenge` directory and run `dotnet run`.

The program consists in a console application that can be run in two modes, interactive and file mode.

To run in file mode, provide your file as a single argument: `dotnet run mycommandfile.txt`. Any other number of arguments will default back to interactive mode.

For the user's convenience, the commands are case insensitive by default, but this can be changed in the `appsettings.json` configuration file, along with some other settings, such as the size of the board.

According to the specifications, the robot is not very chatty, even when things go wrong, so it can be useful to enable the logs if you are lost...

### Prerequisites

* .NET Core 3.1
* .NET Core CLI

## Running the tests

Run `dotnet test`.

## Authors

**Martin Grihangne**

## Acknowledgments

Specifications at https://joneaves.wordpress.com/2014/07/21/toy-robot-coding-test/
Project motivation and inspiration from https://bitbucket.org/simonpb/toy-robot-challenge.
