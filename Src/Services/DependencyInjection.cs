using System.Reflection;
using System.Runtime.CompilerServices;
using AutoMapper;
using Data.Extensions;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Todo.Services.Common.Behaviours;
using Todo.Services.TodoItems.Commands.Actions.CancelItem;
using Todo.Services.TodoItems.Commands.Actions.CompleteItem;
using Todo.Services.TodoItems.Commands.Actions.ResetItem;
using Todo.Services.TodoItems.Commands.Actions.StartItem;
using Todo.Services.TodoItems.Commands.CreateItem;
using Todo.Services.TodoItems.Commands.DeleteItem;
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

            services.AddSingleton(typeof(ICancelItemService), typeof(CancelItemService));
            services.AddSingleton(typeof(ICompleteItemService), typeof(CompleteItemService));
            services.AddSingleton(typeof(IResetItemService), typeof(ResetItemService));
            services.AddSingleton(typeof(IStartItemService), typeof(StartItemService));
            services.AddSingleton(typeof(ICreateItemService), typeof(CreateItemService));
            services.AddSingleton(typeof(IDeleteItemService), typeof(DeleteItemService));
            services.AddSingleton(typeof(IUpdateItemService), typeof(UpdateItemService));
            services.AddSingleton(typeof(IGetItemService), typeof(GetItemService));
            services.AddSingleton(typeof(IChildItemsLookupService), typeof(ChildItemsLookupService));
            services.AddSingleton(typeof(IParentItemsLookupService), typeof(ParentItemsLookupService));

            #endregion Items

            #region Notes

            services.AddSingleton(typeof(ICreateNoteService), typeof(CreateNoteService));
            services.AddSingleton(typeof(IDeleteNoteService), typeof(DeleteNoteService));
            services.AddSingleton(typeof(IUpdateNoteService), typeof(UpdateNoteService));

            #endregion Notes

            return services;
        }
    }
}