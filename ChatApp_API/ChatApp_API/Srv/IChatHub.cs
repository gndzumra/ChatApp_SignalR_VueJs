using System.Threading.Tasks;

namespace ChatApp_API.Srv
{
    public interface IChatHub
    {
        Task ReceiveMessage(string user, string message);
    }
}