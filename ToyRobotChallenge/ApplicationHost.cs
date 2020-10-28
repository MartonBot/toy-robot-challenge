using Common.Log;
using Microsoft.Extensions.Configuration;
using Robot;
using System;
using System.Collections.Generic;

namespace ToyRobotChallenge
{
    class ApplicationHost
    {
        private readonly ILogger _logger;
        private readonly IParser _parser;
        private readonly IConfiguration _config;
        private readonly IRobot _robot;

        public ApplicationHost(ILogger logger, IConfiguration config, IParser parser, IRobot robot)
        {
            _logger = logger;
            _config = config;
            _parser = parser;
            _robot = robot;
        }

        public void Run(string[] args)
        {
            // if an arg is given, treat it as the name of the file to read
            if (args.Length == 1)
            {
                string fileName = args[0];
                FileMode(fileName);
            }
            else
            {
                // else ignore args and enter interactive mode
                InteractiveMode();
            }

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
                _logger.Log($"Failed to parse file {fileName}. Underlying exception: {e.Message}");
                Console.WriteLine("Failed to parse file. The file must be a list of valid robot commands on each line.");
            }

            if (commands != null)
            {
                foreach (ICommand command in commands)
                {
                    string output = _robot.SubmitCommand(command);
                    Console.WriteLine(output);
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
                    Console.WriteLine(output);
                }
                catch (ArgumentException)
                {
                    _logger.Log($"Invalid command: {inputLine}");
                    Console.WriteLine("Failed to parse command. Please enter a command in the form VERB [X, Y, DIRECTION]");
                }
            }
            while (inputLine != "q");
        }
    }
}