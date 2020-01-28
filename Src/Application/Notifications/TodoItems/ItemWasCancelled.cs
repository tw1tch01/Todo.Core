using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Todo.Application.Interfaces;

namespace Todo.Application.Notifications.TodoItems
{
    internal class ItemWasCancelled : INotification
    {
        public ItemWasCancelled(Guid itemId, DateTime cancelledOn)
        {
            ItemId = itemId;
            CancelledOn = cancelledOn;
        }

        public Guid ItemId { get; }

        public DateTime CancelledOn { get; set; }

        internal class SendItemCancelledEmail : INotificationHandler<ItemWasCancelled>
        {
            private readonly IMessageService _notificationService;

            public async Task Handle(ItemWasCancelled notification, CancellationToken cancellationToken)
            {
                await _notificationService.Send($"Item cancelled on {notification.CancelledOn}. (ItemId: '{notification.ItemId}')");
            }
        }
    }
}