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
    }
}
