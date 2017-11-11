using Microsoft.ServiceBus.Messaging;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Threading.Tasks;

namespace FullReceiver
{
    public static class Helpers
    {
        public const string ConnectionStringKey = "Microsoft.ServiceBus.ConnectionString";
        public const string SessionQueueName = "basic-session-queue";
        public const string BasicQueueName = "basic-queue";
        public const string BasicTopicName = "basic-topic";

        public static async Task PrintMessageInfo(BrokeredMessage message)
        {
            Console.WriteLine($"----------------");
            Console.WriteLine($"Label : {message.Label}");
            Console.WriteLine($"Content Type : {message.ContentType}");
            Console.WriteLine($"Time to Live : {message.TimeToLive.TotalMinutes} minutes");

            Stream messageBodyStream = message.GetBody<Stream>();
            string messageString = JToken.Parse(await new StreamReader(messageBodyStream).ReadToEndAsync()).ToString();
            Console.WriteLine($"Message Content {messageString}");
            Console.WriteLine($"----------------");
        }
    }
}
