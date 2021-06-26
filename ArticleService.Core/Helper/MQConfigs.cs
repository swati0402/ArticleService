using Newtonsoft.Json.Linq;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace ArticleService.Core.Helper
{
    public class MQConfigs
    {
        public void PublishToMessageQueue(string integrationEvent, string eventData)
        {
            var factory = new ConnectionFactory();
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            var body = Encoding.UTF8.GetBytes(eventData);
            Console.WriteLine(" [x] Published");
            channel.BasicPublish(exchange: "article",
                                             routingKey: integrationEvent,
                                             basicProperties: null,
                                             body: body);
        }
        public void ListenForIntegrationEvents()
        {
            var factory = new ConnectionFactory();
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(" [x] Received {0}", message);
                var data = JObject.Parse(message);
                var type = ea.RoutingKey;
                SaveData s = new SaveData();
                //Added logic for saving in Sqllite db. But had issues in adding migration. So, just added code logic in here for review.
                s.ProcessessData(data,type);
            };
            channel.BasicConsume(queue: "article.articleservice",
                                     autoAck: true,
                                     consumer: consumer);
        }
    }
}
