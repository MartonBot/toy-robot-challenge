using Robot;
using System;
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
    }
}