using Microsoft.ServiceBus.Messaging;
using System;
using System.Configuration;
using System.Threading.Tasks;

namespace FullReceiver.Receivers
{
    public class BasicQueueReceiver : IDemoMessageReceiver
    {
        //public async Task Receive()
        //{
        //    QueueClient receiveClient = QueueClient.CreateFromConnectionString(ConfigurationManager.AppSettings[Helpers.ConnectionStringKey], Helpers.BasicQueueName);
        //    BrokeredMessage message = await receiveClient.ReceiveAsync();
        //    await Helpers.PrintMessageInfo(message);
        //    await message.Complete();
        //}

        public async Task Receive()
        {
            QueueClient receiveClient = QueueClient.CreateFromConnectionString(ConfigurationManager.AppSettings[Helpers.ConnectionStringKey], Helpers.BasicQueueName);
            var tcs = new TaskCompletionSource<bool>();

            receiveClient.OnMessageAsync(async message =>
            {
                await Helpers.PrintMessageInfo(message);
                await message.CompleteAsync();
            });

            await Task.Delay(TimeSpan.FromMinutes(1));
            tcs.SetResult(true);
            await tcs.Task;
        }
    }
}
