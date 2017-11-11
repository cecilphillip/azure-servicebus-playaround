using System.Threading.Tasks;

namespace FullReceiver
{
    public interface IDemoMessageReceiver
    {
        Task Receive();
    }
}