using Microsoft.Extensions.Configuration;
using Orleans;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;
using Microsoft.Extensions.Logging;
using MineCase.Gateway.Network;
using System.Reflection;
using System.Threading.Tasks;
using Orleans.Runtime;
using Polly;
using Orleans.Configuration;
using Orleans.ApplicationParts;
using Orleans.Hosting;
using Microsoft.Extensions.Hosting;

namespace MineCase.Gateway
{
    partial class Program
    {
        static async Task Main(string[] args)
        {
            var hostBuilder = new HostBuilder()
                .ConfigureAppConfiguration(ConfigureAppConfiguration)
                .ConfigureServices(ConfigureServices)
                .ConfigureLogging(ConfigureLogging)
                .UseConsoleLifetime();

            var host = hostBuilder.Build();
            await host.RunAsync();
        }
    }
}