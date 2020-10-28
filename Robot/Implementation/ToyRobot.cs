using Common;
using Common.Log;
using Microsoft.Extensions.Configuration;
using Robot;
using System;

namespace Implementation.Robot
{
    public class ToyRobot : IRobot
    {
        private const string EMPTY_OUTPUT = "";

        private readonly ILogger _logger;
        private readonly IConfiguration _config;

        private int BoardSizeX { get; set; }
        private int BoardSizeY { get; set; }
        private bool IsOnBoard { get; set; }
        private Vector Position { get; set; }
        private Vector Direction { get; set; }

        public ToyRobot(ILogger logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
            BoardSizeX = _config.GetValue<int>("settings:boardSizeX");
            BoardSizeY = _config.GetValue<int>("settings:boardSizeY");
            IsOnBoard = false;
        }
        public string SubmitCommand(ICommand command)
        {
            switch (command.Verb)
            {
                case "MOVE":
                    return Move();
                case "LEFT":
                    return Left();
                case "RIGHT":
                    return Right();
                case "REPORT":
                    return Report();
                case "PLACE":
                    return Place(command.Position, command.Direction);
                default:
                    throw new InvalidOperationException($"No such command as {command.Verb}");

            }
        }

        private string Place(Vector position, Vector direction)
        {
            bool safe = IsValidPlacement(position);
            if (safe)
            {
                _logger.Log("Safe to place.");
                Position = position;
                Direction = direction;
                IsOnBoard = true;
            }
            else
            {
                _logger.Log($"Unsafe to place. Ignoring command.");
            }
            return EMPTY_OUTPUT;
        }

        private string Move()
        {
            if (IsOnBoard)
            {

                bool safe = IsValidMovement();
                if (safe)
                {
                    _logger.Log("Safe to move.");
                    Position += Direction;
                }
                else
                {
                    _logger.Log($"Unsafe to move. Ignoring command.");
                }
            }
            else
                _logger.Log($"Currently not on the board. Ignoring command.");

            return EMPTY_OUTPUT;
        }

        private string Left()
        {
            if (IsOnBoard)
            {
                Direction = Direction.Left();
            }
            return EMPTY_OUTPUT;
        }

        private string Right()
        {
            if (IsOnBoard)
            {
                Direction = Direction.Right();
            }
            return EMPTY_OUTPUT;
        }

        private string Report()
        {
            return IsOnBoard ? $"{Position.X}, {Position.Y}, {Direction.ToDirectionString()}" : EMPTY_OUTPUT;
        }

        private bool IsValidPlacement(Vector position)
        {
            bool fall = IsValidPosition(position);

            if (fall)
                _logger.Log($"Placement on {position.X}, {position.Y} deemed unsafe.");

            return !fall;
        }

        private bool IsValidMovement()
        {
            Vector positionIfMove = Position + Direction;
            bool fall = IsValidPosition(positionIfMove);
            if (fall)
                _logger.Log($"Movement to {positionIfMove.X}, {positionIfMove.Y} deemed unsafe.");

            return !fall;
        }

        private bool IsValidPosition(Vector position)
        {
            return position.X < 0
                || position.X >= BoardSizeX
                || position.Y < 0
                || position.Y >= BoardSizeY;
        }
    }
}
