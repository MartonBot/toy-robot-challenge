using Microsoft.Extensions.Configuration;

namespace Common.Log
{
    public class ConsoleLogger : ILogger
    {
        private readonly IConfiguration _configuration;

        public ConsoleLogger(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Log(string message)
        {
            if (_configuration.GetValue<bool>("settings:logToConsole"))
            {
                System.Console.WriteLine($"TOY ROBOT LOGS > {message}");
            }
        }
    }
}
