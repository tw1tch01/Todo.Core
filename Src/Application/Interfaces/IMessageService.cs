using System.Threading.Tasks;

namespace Todo.Application.Interfaces
{
    public interface IMessageService
    {
        Task Send(string message);
    }
}