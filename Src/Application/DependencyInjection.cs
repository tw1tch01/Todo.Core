using Microsoft.Extensions.DependencyInjection;
using Todo.Application.Services.TodoItems.CachedItemCommands;
using Todo.Application.Services.TodoItems.CachedItemQueries;
using Todo.Application.Services.TodoItems.ItemCommands;
using Todo.Application.Services.TodoItems.ItemQueries;
using Todo.Application.Services.TodoNotes.NoteCommands;
using Todo.Services;

namespace Todo.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddServices();

            services.AddTransient<IItemsCommandService, ItemsCommandService>();
            services.AddTransient<IItemsQueryService, ItemsQueryService>();
            services.AddTransient<INotesCommandService, NotesCommandService>();
            services.AddTransient<ICachedItemsCommandsService, CachedItemsCommandService>();
            services.AddTransient<ICachedItemsQueryService, CachedItemsQueryService>();

            return services;
        }
    }
}