using EmployeeManagement.API;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace EmployeeManagement.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Invoca a classe Start para configurar a aplicação
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>(); // Invoca a classe Startup para configuração
                });
    }
}

