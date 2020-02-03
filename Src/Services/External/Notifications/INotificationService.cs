using System.Threading.Tasks;
using Todo.DomainModels.Common;

namespace Todo.Services.External.Notifications
{
    public interface INotificationService
    {
        Task Queue(INotificationProcess notification);
    }
}