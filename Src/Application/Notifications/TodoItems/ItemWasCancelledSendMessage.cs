using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Todo.Application.Common;
using Todo.Services.Events.TodoItems;

namespace Todo.Application.Notifications.TodoItems
{
    public class ItemWasCancelledSendMessage : INotificationHandler<ItemWasCancelled>
    {
        private const string _message = "Item cancelled on {0}. (ItemId: '{1}')";

        private readonly IMessageService _messageService;

        public ItemWasCancelledSendMessage(IMessageService messageService)
        {
            _messageService = messageService;
        }

        public async Task Handle(ItemWasCancelled notification, CancellationToken cancellationToken)
        {
            await _messageService.Send(string.Format(_message, notification.CancelledOn, notification.ItemId));
        }
    }
}