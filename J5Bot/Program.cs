using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using RG.Bot;

namespace J5Bot
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Hacked in, this needs improving.
            new Bot();

            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
