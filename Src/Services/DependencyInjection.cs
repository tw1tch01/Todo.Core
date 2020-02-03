using System.Reflection;
using System.Runtime.CompilerServices;
using AutoMapper;
using Data.Extensions;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Todo.Services.Common.Behaviours;
using Todo.Services.External.Notifications;
using Todo.Services.External.Workflows;
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
            services.AddValidatorsFromAssembly(assembly);

            AddServicesImplementations(services);

            return services;
        }

        public static IServiceCollection AddPipelineBehaviour(this IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformance<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidation<,>));
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            return services;
        }

        private static IServiceCollection AddServicesImplementations(IServiceCollection services)
        {
            #region Items

            services.AddSingleton<ICancelItemService, CancelItemService>();
            services.AddSingleton<ICompleteItemService, CompleteItemService>();
            services.AddSingleton<IResetItemService, ResetItemService>();
            services.AddSingleton<IStartItemService, StartItemService>();
            services.AddSingleton<ICreateItemService, CreateItemService>();
            services.AddSingleton<IDeleteItemService, DeleteItemService>();
            services.AddSingleton<IUpdateItemService, UpdateItemService>();
            services.AddSingleton<IGetItemService, GetItemService>();
            services.AddSingleton<IChildItemsLookupService, ChildItemsLookupService>();
            services.AddSingleton<IParentItemsLookupService, ParentItemsLookupService>();

            #endregion Items

            #region Notes

            services.AddSingleton<ICreateNoteService, CreateNoteService>();
            services.AddSingleton<IDeleteNoteService, DeleteNoteService>();
            services.AddSingleton<IUpdateNoteService, UpdateNoteService>();

            #endregion Notes

            #region External

            services.AddSingleton<INotificationService, NotificationService>();
            services.AddSingleton<IWorkflowService, WorkflowService>();

            #endregion External

            return services;
        }
    }
}