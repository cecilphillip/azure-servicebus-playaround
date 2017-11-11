using FullSender.Senders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullSender
{
    class Program
    {
        static async Task Main(string[] args)
        {
            IDemoMessageSender sender = new BasicTopicSender();

            try
            {
                await sender.Send();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
