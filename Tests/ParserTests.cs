using Robot;
using System;
using System.Collections.Generic;
using Xunit;

namespace Tests
{
    public class ParserTests
    {
        private readonly IParser _parser;

        public ParserTests(IParser parser)
        {
            _parser = parser;
        }

        [Theory]
        [InlineData("REPORT")]
        [InlineData("MOVE")]
        [InlineData("LEFT")]
        [InlineData("RIGHT")]
        public void Parse_WhenValidSimpleCommand_ShouldReturnCommand(string simpleCommand)
        {
            ICommand command = _parser.Parse(simpleCommand);
            Assert.NotNull(command);
        }

        [Theory]
        [InlineData("PLACE")]
        [InlineData("JUMP")]
        [InlineData("567")]
        public void Parse_WhenInvalidSimpleCommand_ShouldThrowArgumentException(string simpleCommand)
        {
            Assert.Throws<ArgumentException>(() => _parser.Parse(simpleCommand));
        }

        [Theory]
        [InlineData("PLACE 1, 3, MELBOURNE")]
        [InlineData("PLACE 1, go, SOUTH")]
        [InlineData("PLACE hello, 3, EAST")]
        [InlineData("PLACE 1, 7777777777777777777, EAST")]
        public void Parse_WhenPlaceCommandWithInvalidParams_ShouldThrowArgumentException(string input)
        {
            Assert.Throws<ArgumentException>(() => _parser.Parse(input));
        }

        [Theory]
        [InlineData("PLACE 1, 3, WEST", "WEST")]
        [InlineData("PLACE 1,3, WEST", "WEST")]
        [InlineData("PLACE 1,3,WEST", "WEST")]
        public void Parse_WhenValidPlaceCommand_ShouldRegisterParameters(string input, string expectedDirection)
        {
            ICommand command = _parser.Parse(input);
            Assert.NotNull(command);
            Assert.Equal(expectedDirection, command.Direction.ToDirectionString());
            Assert.NotNull(command.Position);
            Assert.NotNull(command.Direction);
        }
    }
}
