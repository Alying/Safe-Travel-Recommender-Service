namespace Service
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Hosting;

    /// <summary>
    /// Main program for safe-travel service.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main.
        /// </summary>
        /// <param name="args">system arguments.</param>
        public static void Main(string[] args) => CreateHostBuilder(args).Build().Run();

        /// <summary>
        /// Creates and configures web host.
        /// </summary>
        /// <param name="args">arguments.</param>
        /// <returns>host builder.</returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
