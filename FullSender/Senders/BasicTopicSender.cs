using Microsoft.ServiceBus.Messaging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullSender.Senders
{
    public class BasicTopicSender : IDemoMessageSender
    {
        public async Task Send()
        {
            MessagingFactory factory = MessagingFactory.CreateFromConnectionString(ConfigurationManager.AppSettings[Helpers.ConnectionStringKey]);
            TopicClient topicClient = factory.CreateTopicClient(Helpers.BasicTopicName);

            byte[] messageData = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(Helpers.GetModels()));

            BrokeredMessage message = new BrokeredMessage(new MemoryStream(messageData), true)
            {
                ContentType = "application/json",
                Label = "dynamic topic data",
                TimeToLive = TimeSpan.FromMinutes(20)
            };

            await topicClient.SendAsync(message);
        }
    }
}
