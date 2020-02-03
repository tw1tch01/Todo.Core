using System.Threading.Tasks;

namespace Todo.Services.External.Notifications
{
    public interface INotificationService
    {
        Task Queue(INotificationProcess notification);
    }
}