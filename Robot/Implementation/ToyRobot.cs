﻿using Common;
using Common.Log;
using Microsoft.Extensions.Configuration;
using Robot;
using Robot.Geometry;
using System;
using System.Collections.Generic;

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

            // the robot's knowledge of the board dimensions
            BoardSizeX = _config.GetValue<int>("settings:boardSizeX");
            BoardSizeY = _config.GetValue<int>("settings:boardSizeY");

            // initially the robot is somewhere else
            IsOnBoard = false;
        }

        public string SubmitCommand(ICommand command)
        {
            // if this had to scale to a lot more commands, we could use a dictionary of delegates here
            switch (command.Verb)
            {
                case "MOVE":
                    return DoIfOnBoard(Move);
                case "LEFT":
                    return DoIfOnBoard(Left);
                case "RIGHT":
                    return DoIfOnBoard(Right);
                case "REPORT":
                    return DoIfOnBoard(Report);
                case "PLACE":
                    return Place(command.Position, command.Direction);
                default:
                    throw new InvalidOperationException($"No such command as {command.Verb}");

            }
        }

        private string DoIfOnBoard(RobotAction action)
        {
            string output = null;
            if (IsOnBoard)
            {
                output = action();
            }
            else
            {
                _logger.Log($"Currently not on the board. Ignoring command.");
            }
            return output;
        }

        private string Move()
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
            return null;
        }

        private string Left()
        {
            Direction = Direction.Left();
            return null;
        }

        private string Right()
        {
            Direction = Direction.Right();
            return null;
        }

        private string Report()
        {
            return $"{Position.X}, {Position.Y}, {Direction.ToDirectionString()}";
        }

        private string Place(Vector position, Vector direction)
        {
            // nothing says the robot can't be placed again somewhere else when it is already on the board
            bool safe = IsValidPlacement(position);
            if (safe)
            {
                _logger.Log($"Safe to place at {position.X}, ${position.Y}.");
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

    // Declare a delegate type for commands that can be actioned only when the robot is on the board
    delegate string RobotAction();
}
