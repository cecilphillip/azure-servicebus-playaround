using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace FullSender.Senders
{
    public class RequestResponseSender : IDemoMessageSender
    {
        public async Task Send()
        {
            await CreateResponseQueue();

            QueueClient sendClient = QueueClient.CreateFromConnectionString(ConfigurationManager.AppSettings[Helpers.ConnectionStringKey], Helpers.BasicQueueName);
            QueueClient responseClient = QueueClient.CreateFromConnectionString(ConfigurationManager.AppSettings[Helpers.ConnectionStringKey], Helpers.ResponseQueue);

            byte[] messageData = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(Helpers.GetModels()));
            string sessionId = Guid.NewGuid().ToString("N");

            BrokeredMessage message = new BrokeredMessage(new MemoryStream(messageData), true)
            {
                ContentType = "application/json",
                Label = "dynamic data",
                TimeToLive = TimeSpan.FromMinutes(20),
                ReplyToSessionId = sessionId,
                ReplyTo = Helpers.ResponseQueue
            };

            await sendClient.SendAsync(message);

            MessageSession messageSession = await responseClient.AcceptMessageSessionAsync(sessionId);
            BrokeredMessage responseMessage = await messageSession.ReceiveAsync();

            string responseMessageBody = responseMessage.GetBody<string>();            
            Console.WriteLine($"Message response received: \n{responseMessageBody}");
        }

        private async Task CreateResponseQueue()
        {
            NamespaceManager manager = NamespaceManager.CreateFromConnectionString(ConfigurationManager.AppSettings[Helpers.ConnectionStringKey]);
            if (!manager.QueueExists(Helpers.ResponseQueue))
            {
                QueueDescription queueDescription = new QueueDescription(Helpers.ResponseQueue)
                {
                    RequiresSession = true
                };

                await manager.CreateQueueAsync(queueDescription);
            }
        }
    }
}
