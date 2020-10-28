using Common;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Robot
{
    public class Parser : IParser
    {
        private const string report = "REPORT";
        private const string move = "MOVE";
        private const string left = "LEFT";
        private const string right = "RIGHT";
        private const string place = "PLACE";
        private const string north = "NORTH";
        private const string south = "SOUTH";
        private const string east = "EAST";
        private const string west = "WEST";
        private const string intPattern = @"\d{1,9}";
        private static string commandPattern = @$"(?'verb'{report}|{move}|{left}|{right})|((?'verb'{place}) (?'xpos'{intPattern}), ?(?'ypos'{intPattern}), ?(?'direction'{north}|{south}|{east}|{west}))";
        private Regex commandRegex = new Regex(commandPattern);

        private readonly IConfiguration _config;

        public Parser(IConfiguration config)
        {
            _config = config;
        }

        /// <summary>
        /// Turns a string into a robot command
        /// </summary>
        ///
        public ICommand Parse(string input)
        {
            if (!_config.GetValue<bool>("settings:caseSensitive"))
            {
                input = input.ToUpper();
            }
            Match match = commandRegex.Match(input);

            if (!match.Success)
                throw new ArgumentException($"Not a valid input: {input}");

            return BuildCommand(match);
        }

        public List<ICommand> ParseFile(string fileName)
        {
            throw new NotImplementedException();
        }

        private Command BuildCommand(Match match)
        {
            string verb = match.Groups["verb"].Value;

            switch (verb)
            {
                case move:
                case report:
                case left:
                case right:
                    return new Command(verb);

                case place:
                    int xpos = int.Parse(match.Groups["xpos"].Value);
                    int ypos = int.Parse(match.Groups["ypos"].Value);
                    Vector position = new Vector(xpos, ypos);
                    string strDirection = match.Groups["direction"].Value;
                    Vector direction = ParseDirection(strDirection);
                    return new Command(verb, position, direction);

                default:
                    throw new ArgumentException($"Not a valid command verb: {verb}");
            }
        }

        private Vector ParseDirection(string direction)
        {
            switch (direction)
            {
                case north:
                    return Vector.NORTH;
                case south:
                    return Vector.SOUTH;
                case east:
                    return Vector.EAST;
                case west:
                    return Vector.WEST;
                default:
                    throw new InvalidOperationException($"Not a valid direction: {direction}");
            }

        }
    }
}
