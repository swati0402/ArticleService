using ArticleService.Core.Helper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace ArticleConsumerService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ConsumeService();
            CreateHostBuilder(args).Build().Run();
        }
        public static void ConsumeService()
        {
            MQConfigs mq = new MQConfigs();
            mq.ListenForIntegrationEvents();
        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
