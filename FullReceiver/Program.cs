using FullReceiver.Receivers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullReceiver
{
    class Program
    {
        static async Task Main(string[] args)
        {

            IDemoMessageReceiver receiver = new RequestResponseReceiver();
            try
            {
                var random = new Random();
                var consoleColors = Enum.GetValues(typeof(ConsoleColor));
                Console.ForegroundColor = (ConsoleColor)consoleColors.GetValue(random.Next(consoleColors.Length));
            
                await receiver.Receive();
                Console.ReadLine();
            }
            catch (Exception ex)
            {
            }
            finally {
                Console.ResetColor();
            }
        }
    }


}
