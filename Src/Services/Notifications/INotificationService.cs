using System.Threading.Tasks;

namespace Todo.Services.Notifications
{
    public interface INotificationService
    {
        Task Queue(INotificationProcess notification);
    }
}