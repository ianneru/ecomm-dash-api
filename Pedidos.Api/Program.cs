using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace Pedidos.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Console.Title = "Pedidos.API";

            IWebHost host = BuildWebHost(args);

            await SeedData.EnsureSeedData(host.Services);

            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args)
        {
            return WebHost
                    .CreateDefaultBuilder(args)
                    .UseStartup<Startup>()
                    .Build();
        }
    }
}
