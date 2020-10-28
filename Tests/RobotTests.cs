using Robot;
using Xunit;

namespace Tests
{
    public class RobotTests
    {
        private readonly IRobot _robot;
        private readonly IParser _parser;

        public RobotTests(IRobot robot, IParser parser)
        {
            _robot = robot;
            _parser = parser;
        }

        [Theory]
        [InlineData("0, 0, EAST", "1, 0, EAST")]
        [InlineData("0, 0, NORTH", "0, 1, NORTH")]
        [InlineData("2, 2, WEST", "1, 2, WEST")]
        public void ProcessCommand_Move_WhenNoObstacle_ShouldMove(string initial, string final)
        {
            var placeCommand = _parser.Parse($"PLACE {initial}");
            _robot.SubmitCommand(placeCommand);
            var move = _parser.Parse("MOVE");
            _robot.SubmitCommand(move);
            var report = _parser.Parse("REPORT");
            var output = _robot.SubmitCommand(report);

            Assert.Equal(final, output);
        }

        [Theory]
        [InlineData("4, 4, NORTH", "4, 4, NORTH")]
        [InlineData("0, 2, WEST", "0, 2, WEST")]
        [InlineData("3, 0, SOUTH", "3, 0, SOUTH")]
        public void ProcessCommand_Move_WhenWouldFall_ShouldNotMove(string initial, string final)
        {
            var placeCommand = _parser.Parse($"PLACE {initial}");
            _robot.SubmitCommand(placeCommand);
            var move = _parser.Parse("MOVE");
            _robot.SubmitCommand(move);
            var report = _parser.Parse("REPORT");
            var output = _robot.SubmitCommand(report);

            Assert.Equal(final, output);
        }

        [Theory]
        [InlineData("4, 4, NORTH", "4, 4, WEST")]
        [InlineData("0, 2, WEST", "0, 2, SOUTH")]
        [InlineData("3, 0, SOUTH", "3, 0, EAST")]
        public void ProcessCommand_Left_ShouldTurnLeft(string initial, string final)
        {
            var placeCommand = _parser.Parse($"PLACE {initial}");
            _robot.SubmitCommand(placeCommand);
            var move = _parser.Parse("LEFT");
            _robot.SubmitCommand(move);
            var report = _parser.Parse("REPORT");
            var output = _robot.SubmitCommand(report);

            Assert.Equal(final, output);
        }

        [Theory]
        [InlineData("4, 4, NORTH", "4, 4, EAST")]
        [InlineData("0, 2, WEST", "0, 2, NORTH")]
        [InlineData("3, 0, SOUTH", "3, 0, WEST")]
        public void ProcessCommand_Right_ShouldTurnRight(string initial, string final)
        {
            _robot.SubmitCommand(_parser.Parse($"PLACE {initial}"));
            var move = _parser.Parse("RIGHT");
            _robot.SubmitCommand(move);
            var output = _robot.SubmitCommand(_parser.Parse("REPORT"));

            Assert.Equal(final, output);
        }
    }
}
