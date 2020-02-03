using System.Threading.Tasks;
using Todo.Services.Common;

namespace Todo.Services.External.Notifications
{
    public interface INotificationService
    {
        Task Queue(INotificationProcess notification);
    }
}