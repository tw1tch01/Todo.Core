using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Todo.Application.Notifications.TodoItems;
using Todo.Application.Services.TodoItems.ItemCommands;
using Todo.Services;
using Todo.Services.Events.TodoItems;

namespace Todo.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddServices();
            services.AddScoped<IItemsCommandService, ItemsCommandService>();

            //RegisterNotifications(services);

            return services;
        }

        private static void RegisterNotifications(IServiceCollection services)
        {
            services.AddTransient<INotificationHandler<ItemWasCancelled>, ItemWasCancelledSendMessage>();
        }
    }
}