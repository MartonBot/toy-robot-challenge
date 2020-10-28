using Robot;
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
    }
}
