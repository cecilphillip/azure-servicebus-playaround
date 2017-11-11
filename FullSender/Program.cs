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
            IDemoMessageSender sender = new RequestResponseSender();

            try
            {
                await sender.Send();
                Console.ReadLine();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
