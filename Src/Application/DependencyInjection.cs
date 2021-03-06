﻿using Microsoft.Extensions.DependencyInjection;
using Todo.Application.Services.TodoItems;
using Todo.Application.Services.TodoNotes;
using Todo.Services;

namespace Todo.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddServices();

            services.AddScoped<ItemsCommandService>();
            services.AddScoped<ItemsQueryService>();
            services.AddScoped<NotesCommandService>();

            return services;
        }
    }
}