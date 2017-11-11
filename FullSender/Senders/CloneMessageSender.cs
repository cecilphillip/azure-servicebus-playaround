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
    public class CloneMessageSender : IDemoMessageSender
    {
        public async Task Send()
        {
            QueueClient sendClient = QueueClient.CreateFromConnectionString(ConfigurationManager.AppSettings[Helpers.ConnectionStringKey], Helpers.BasicQueueName);
            byte[] messageData = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(Helpers.GetModels()));

            BrokeredMessage message = new BrokeredMessage(new MemoryStream(messageData), true)
            {
                ContentType = "application/json",
                Label = "dynamic data",
                TimeToLive = TimeSpan.FromMinutes(20),
            };
            message.Properties.Add("MyCustomSetting", "Setting value");

            await sendClient.SendAsync(message);
            message = message.Clone();
            await sendClient.SendAsync(message);
        }
    }
}
