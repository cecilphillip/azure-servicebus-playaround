using Microsoft.ServiceBus.Messaging;
using System;
using System.Configuration;
using System.Threading.Tasks;

namespace FullReceiver.Receivers
{
    public class RequestResponseReceiver : IDemoMessageReceiver
    {

        public async Task Receive()
        {
            QueueClient receiveClient = QueueClient.CreateFromConnectionString(ConfigurationManager.AppSettings[Helpers.ConnectionStringKey], Helpers.BasicQueueName);
            
            BrokeredMessage message = await receiveClient.ReceiveAsync();
            await Helpers.PrintMessageInfo(message);
            await message.CompleteAsync();

            BrokeredMessage response = new BrokeredMessage("Response from Receiver") {
                SessionId = message.ReplyToSessionId
            };

            QueueClient sendClient = QueueClient.CreateFromConnectionString(ConfigurationManager.AppSettings[Helpers.ConnectionStringKey], message.ReplyTo);

            await sendClient.SendAsync(response);
        }
    }
}
