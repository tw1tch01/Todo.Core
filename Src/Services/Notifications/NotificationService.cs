using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Todo.Services.Notifications
{
    public class NotificationService : Mediator
    {
        private readonly Func<IEnumerable<Func<INotification, CancellationToken, Task>>, INotification, CancellationToken, Task> _publish;

        public NotificationService(ServiceFactory serviceFactory) : base(serviceFactory)
        {
            _publish = ParallelNoWait;
        }

        protected override Task PublishCore(IEnumerable<Func<INotification, CancellationToken, Task>> allHandlers, INotification notification, CancellationToken cancellationToken)
        {
            return _publish(allHandlers, notification, cancellationToken);
        }

        private static Task ParallelNoWait(IEnumerable<Func<INotification, CancellationToken, Task>> handlers, INotification notification, CancellationToken cancellationToken)
        {
            foreach (var handler in handlers)
            {
                Task.Run(() => handler(notification, cancellationToken));
            }

            return Task.CompletedTask;
        }
    }
}