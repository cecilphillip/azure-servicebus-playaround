using Microsoft.ServiceBus.Messaging;
using System;
using System.Configuration;
using System.Threading.Tasks;

namespace FullReceiver.Receivers
{
    public class QueueSessionReceiver : IDemoMessageReceiver
    {
        public async Task Receive()
        {
            QueueClient receiveClient = QueueClient.CreateFromConnectionString(ConfigurationManager.AppSettings[Helpers.ConnectionStringKey], Helpers.SessionQueueName);
            var tcs = new TaskCompletionSource<bool>();

            MessageSession messageSession = await receiveClient.AcceptMessageSessionAsync();

            OnMessageOptions options = new OnMessageOptions
            {
                AutoComplete = false,
                MaxConcurrentCalls = 4
            };

            messageSession.OnMessageAsync(async message =>
            {
                await Helpers.PrintMessageInfo(message);
                await message.CompleteAsync();
            }, options);

            await Task.Delay(TimeSpan.FromMinutes(1));
            tcs.SetResult(true);
            await tcs.Task;
        }
    }
}
