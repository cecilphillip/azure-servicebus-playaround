using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using System;
//using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullReceiver.Receivers
{
    public class BasicTopicReceiver : IDemoMessageReceiver
    {
        //public async Task Receive()
        //{
        //    string subscriptionName = Guid.NewGuid().ToString("N");
        //    MessagingFactory factory = MessagingFactory.CreateFromConnectionString(ConfigurationManager.AppSettings[Helpers.ConnectionStringKey]);

        //    // Create subscription
        //    SubscriptionDescription description = new SubscriptionDescription(Helpers.BasicTopicName, subscriptionName)
        //    {
        //        AutoDeleteOnIdle = TimeSpan.FromMinutes(5)
        //    };

        //    NamespaceManager manager = NamespaceManager.CreateFromConnectionString(ConfigurationManager.AppSettings[Helpers.ConnectionStringKey]);

        //    SubscriptionClient subscriptionClient = factory.CreateSubscriptionClient(Helpers.BasicTopicName, subscriptionName);
        //    await manager.CreateSubscriptionAsync(description);

        //    // Receive message
        //    BrokeredMessage message = await subscriptionClient.ReceiveAsync();

        //    await Helpers.PrintMessageInfo(message);
        //}


        public async Task Receive()
        {
            string subscriptionName = Guid.NewGuid().ToString("N");
            MessagingFactory factory = MessagingFactory.CreateFromConnectionString(ConfigurationManager.AppSettings[Helpers.ConnectionStringKey]);

            // Create subscription
            SubscriptionDescription description = new SubscriptionDescription(Helpers.BasicTopicName, subscriptionName)
            {
                AutoDeleteOnIdle = TimeSpan.FromMinutes(5)
            };

            NamespaceManager manager = NamespaceManager.CreateFromConnectionString(ConfigurationManager.AppSettings[Helpers.ConnectionStringKey]);            
            await manager.CreateSubscriptionAsync(description);

            // Receive messagea.
            var tcs = new TaskCompletionSource<bool>();

            SubscriptionClient subscriptionClient = factory.CreateSubscriptionClient(Helpers.BasicTopicName, subscriptionName);
            subscriptionClient.OnMessageAsync(async message =>
            {
                await Helpers.PrintMessageInfo(message);
            });

            await Task.Delay(TimeSpan.FromMinutes(1));
            tcs.SetResult(true);
            await tcs.Task;
        }
    }
}
