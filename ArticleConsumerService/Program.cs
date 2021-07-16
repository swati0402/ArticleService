using ArticleService.Core.Helper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;

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
            string msg= mq.ListenForIntegrationEvents();
            Console.WriteLine(" [x] ConsumeService:", msg);
        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
