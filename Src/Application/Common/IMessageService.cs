using System.Threading.Tasks;

namespace Todo.Application.Common
{
    public interface IMessageService
    {
        Task Send(string message);
    }
}