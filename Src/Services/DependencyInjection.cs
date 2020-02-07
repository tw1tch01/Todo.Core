using System.Reflection;
using System.Runtime.CompilerServices;
using AutoMapper;
using Data.Extensions;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Todo.Services.Notifications;
using Todo.Services.Workflows;
using Todo.Services.TodoItems.Commands.CancelItem;
using Todo.Services.TodoItems.Commands.CompleteItem;
using Todo.Services.TodoItems.Commands.CreateItem;
using Todo.Services.TodoItems.Commands.DeleteItem;
using Todo.Services.TodoItems.Commands.ResetItem;
using Todo.Services.TodoItems.Commands.StartItem;
using Todo.Services.TodoItems.Commands.UpdateItem;
using Todo.Services.TodoItems.Queries.GetItem;
using Todo.Services.TodoItems.Queries.Lookups.ChildItems;
using Todo.Services.TodoItems.Queries.Lookups.ParentItems;
using Todo.Services.TodoNotes.Commands.CreateNote;
using Todo.Services.TodoNotes.Commands.DeleteNote;
using Todo.Services.TodoNotes.Commands.UpdateNote;

[assembly: InternalsVisibleTo("Todo.Services.UnitTests")]

namespace Todo.Services
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();

            services.AddDataDependencies();

            services.AddAutoMapper(assembly);
            services.AddMediatR(assembly);

            AddServicesImplementations(services);

            return services;
        }

        public static IServiceCollection AddValidation(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            return services;
        }

        private static IServiceCollection AddServicesImplementations(IServiceCollection services)
        {
            #region Items

            // Commands
            services.AddTransient<ICancelItemService, CancelItemService>();
            services.AddTransient<ICompleteItemService, CompleteItemService>();
            services.AddTransient<IResetItemService, ResetItemService>();
            services.AddTransient<IStartItemService, StartItemService>();
            services.AddTransient<ICreateItemService, CreateItemService>();
            services.AddTransient<IDeleteItemService, DeleteItemService>();
            services.AddTransient<IUpdateItemService, UpdateItemService>();

            // Queries
            services.AddTransient<IGetItemService, GetItemService>();
            services.AddTransient<IChildItemsLookupService, ChildItemsLookupService>();
            services.AddTransient<IParentItemsLookupService, ParentItemsLookupService>();

            #endregion Items

            #region Notes

            // Commands
            services.AddTransient<ICreateNoteService, CreateNoteService>();
            services.AddTransient<IDeleteNoteService, DeleteNoteService>();
            services.AddTransient<IUpdateNoteService, UpdateNoteService>();

            #endregion Notes

            #region External

            services.AddTransient<INotificationService, NotificationService>();
            services.AddTransient<IWorkflowService, WorkflowService>();

            #endregion External

            return services;
        }
    }
}