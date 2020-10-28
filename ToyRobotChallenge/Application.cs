using Common.Log;
using Microsoft.Extensions.Configuration;
using Robot;
using System;
using System.Collections.Generic;

namespace ToyRobotChallenge
{
    class Application
    {
        private readonly ILogger _logger;
        private readonly IParser _parser;
        private readonly IConfiguration _config;
        private readonly IRobot _robot;

        public Application(ILogger logger, IConfiguration config, IParser parser, IRobot robot)
        {
            _logger = logger;
            _config = config;
            _parser = parser;
            _robot = robot;
        }

        public void Run(string[] args)
        {
            if (args.Length == 1)
            {
                // if an arg is given, treat it as the name of the file to read
                string fileName = args[0];
                FileMode(fileName);
            }
            else
            {
                // else ignore args and enter interactive mode
                InteractiveMode();
            }

            // end of the application
            _logger.Log("Host shutdown.");
        }

        private void FileMode(string fileName)
        {
            List<ICommand> commands = null;
            try
            {
                commands = _parser.ParseFile(fileName);
                _logger.Log($"Parsed a total {commands.Count} commands from file {fileName}");
            }
            catch (Exception e)
            {
                // it's not ideal to catch a general exception here, but there are so many underlying exceptions - maybe add an exception filter
                _logger.Log($"Failed to parse file {fileName}. Underlying exception: {e.Message}");
                Console.WriteLine("Failed to parse file. The file must be a list of valid robot commands on each line.");
            }

            if (commands != null)
            {
                // this could be one line in LINQ, but is arguably more readable this way
                foreach (ICommand command in commands)
                {
                    string output = _robot.SubmitCommand(command);
                    WriteRobotOutputToConsole(output);
                }
            }
        }

        private void InteractiveMode()
        {
            Console.WriteLine("Interactive mode. Please enter a command or q to exit.");

            string inputLine;
            ICommand command;
            string prompt = _config.GetValue<string>("settings:interactivePrompt");

            do
            {
                Console.Write(prompt);
                inputLine = Console.ReadLine();

                if (inputLine == "q")
                    break;

                try
                {
                    command = _parser.Parse(inputLine);
                    _logger.Log($"Parsed command: {command.Verb}");
                    string output = _robot.SubmitCommand(command);
                    WriteRobotOutputToConsole(output);
                }
                catch (ArgumentException)
                {
                    _logger.Log($"Invalid command: {inputLine}");
                    Console.WriteLine("Failed to parse command. Please enter one of:");
                    Console.WriteLine("PLACE <x>, <y>, <direction>");
                    Console.WriteLine("MOVE");
                    Console.WriteLine("LEFT");
                    Console.WriteLine("RIGHT");
                    Console.WriteLine("REPORT");
                }
            }
            while (inputLine != "q");
        }

        private void WriteRobotOutputToConsole(string output)
        {
            // do not print anything to the console if the robot's output is empty
            if (!string.IsNullOrWhiteSpace(output))
                Console.WriteLine(output);
        }
    }
}