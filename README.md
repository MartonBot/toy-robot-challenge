[![Build Status](https://dev.azure.com/Altirion/Toy%20Robot%20Challenge/_apis/build/status/MartonBot.toy-robot-challenge?branchName=main)](https://dev.azure.com/Altirion/Toy%20Robot%20Challenge/_build/latest?definitionId=13&branchName=main)

# Martin's Toy Robot Challenge

I was asked to do the toy robot challenge for an interview. Here is my implementation.

## Getting Started

From the project's root directory, run `cd ToyRobotChallenge` and `dotnet run`.

The program consists in a console application that can be run in two modes, interactive and file mode.

To run in file mode, provide your file as a single argument: `dotnet run somecommands.txt`. Any other number of arguments will default back to interactive mode.

For the user's convenience, the commands are case insensitive by default, but this can be changed in the `appsettings.json` configuration file of the `ToyRobotChallenge` project, along with some other settings, such as the size of the board or whether to display useful logs.

According to the specifications, the robot is not very chatty, even when things go wrong, so it can be useful to enable the logs if you are lost...

### Prerequisites

* .NET Core 3.1
* .NET Core CLI

## Running the tests

Run `cd toy-robot-challenge` and `dotnet test`.

## Authors

**Martin Grihangne**

## Acknowledgments

* Specifications at https://joneaves.wordpress.com/2014/07/21/toy-robot-coding-test/
* Project motivation and inspiration from https://bitbucket.org/simonpb/toy-robot-challenge.
