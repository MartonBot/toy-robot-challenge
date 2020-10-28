using Common.Log;
using Implementation.Robot;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Robot;
using System;
using System.Diagnostics;
using System.IO;

namespace ToyRobotChallenge
{
    class Program
    {
        static void Main(string[] args)
        {
            // getting the configuration from appsettings.json
            var configBuilder = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appsettings.json",
                 optional: false,
                 reloadOnChange: true);
            var config = configBuilder.Build();

            // registering dependency injection services
            var services = new ServiceCollection()
                .AddSingleton<Application>()
                .AddSingleton<ILogger, ConsoleLogger>()
                .AddSingleton<IConfiguration>(config)
                .AddSingleton<IParser, Parser>()
                .AddTransient<IRobot, ToyRobot>()
                .BuildServiceProvider();

            // handling unhandled exceptions
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(UnhandledExceptionHandler);

            // running the console app
            services.GetService<Application>().Run(args);
        }
        static void UnhandledExceptionHandler(object sender, UnhandledExceptionEventArgs e)
        {
            Debug.WriteLine((e.ExceptionObject as Exception).Message);
        }
    }
}
