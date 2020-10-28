# Martin's Toy Robot Challenge

I was asked to do the toy robot challenge for an interview. Here is my crack at it.

## Getting Started

Clone the project, `cd` into the `ToyRobotChallenge` directory and run `dotnet run`.

The program consists in a console application that can be run in two modes, interactive and file.
The interactive mode is the default mode.
To run in file mode, provide your file as a single argument: `dotnet run myfile.txt`. Any other number of arguments will default back to interactive mode.

For the user's convenience, the commands are case insensitive, but this can be changed in the `appsettings.json` configuration file, along with some other settings.

### Prerequisites

.NET Core 3.1

## Running the tests

Run `dotnet test`.

## Authors

* **Martin Grihangne**

## Acknowledgments

* Project motivation and inspiration from https://bitbucket.org/simonpb/toy-robot-challenge.
