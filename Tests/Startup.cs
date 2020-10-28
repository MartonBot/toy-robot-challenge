using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Robot;
using System.IO;

namespace Tests
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            var configBuilder = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appsettings.json",
                 optional: false,
                 reloadOnChange: true);
            var config = configBuilder.Build();

            services.AddSingleton<IConfiguration>(config)
                .AddTransient<IParser, Parser>()
                .AddTransient<IRobot, ToyRobot>();
        }
    }
}
