using Microsoft.ServiceBus.Messaging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace FullSender.Senders
{
    public class QueueSessionSender : IDemoMessageSender
    {
        public async Task Send()
        {
            List<BrokeredMessage> messages = new List<BrokeredMessage>();
            QueueClient sendClient = QueueClient.CreateFromConnectionString(ConfigurationManager.AppSettings[Helpers.ConnectionStringKey], Helpers.SessionQueueName);
            string sessionID = Guid.NewGuid().ToString("N");

            foreach (dynamic item in Helpers.GetModels())
            {
                byte[] messageData = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(item));

                BrokeredMessage message = new BrokeredMessage(new MemoryStream(messageData), true)
                {
                    SessionId = sessionID,
                    ContentType = "application/json",
                    Label = "dynamic data",
                    TimeToLive = TimeSpan.FromMinutes(20)
                };
                messages.Add(message);
            }

            await sendClient.SendBatchAsync(messages);
        }
    }
}
