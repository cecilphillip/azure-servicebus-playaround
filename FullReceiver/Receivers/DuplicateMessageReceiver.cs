using Microsoft.ServiceBus.Messaging;
using System;
using System.Configuration;
using System.Threading.Tasks;

namespace FullReceiver.Receivers
{
    public class DuplicateMessageReceiver : IDemoMessageReceiver
    {
        public async Task Receive()
        {
            QueueClient receiveClient = QueueClient.CreateFromConnectionString(ConfigurationManager.AppSettings[Helpers.ConnectionStringKey], Helpers.BasicQueueName, ReceiveMode.ReceiveAndDelete);
            var tcs = new TaskCompletionSource<bool>();

            receiveClient.OnMessageAsync(async message =>
            {
                Console.WriteLine($"Receiving message with ID {message.MessageId}");
                await Helpers.PrintMessageInfo(message);
                await message.CompleteAsync();
            });

            await Task.Delay(TimeSpan.FromMinutes(1));
            tcs.SetResult(true);
            await tcs.Task;
        }
    }
}
