using Microsoft.Extensions.DependencyInjection;
using Todo.Application.Services.TodoItems.ItemCommands;
using Todo.Services;

namespace Todo.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddServices();
            services.AddScoped<IItemsCommandService, ItemsCommandService>();

            return services;
        }
    }
}