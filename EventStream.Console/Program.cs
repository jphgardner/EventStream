using System;
using System.IO;
using System.Threading.Tasks;
using EventStream.Domain;
using EventStream.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EventStream.Console
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json", false)
                .Build();
            
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .AddSingleton<IConfigurationRoot>(configuration)
                .AddEventStream(configuration)
                .BuildServiceProvider();

            IEventStreamClient eventStreamClient = serviceProvider.GetService<IEventStreamClient>();

            System.Console.ReadLine();
        }
    }
}