using System.Reflection;
using AutoMapper;
using Data.Extensions;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Todo.Application.Interfaces.TodoItems;
using Todo.Application.Services.TodoItems;

namespace Todo.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCommonApplicationServices(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();
            services.AddDataDependencies();
            services.AddAutoMapper(assembly);
            services.AddMediatR(assembly);
            services.AddValidatorsFromAssembly(assembly);

            services.AddScoped(typeof(IItemsCommandService), typeof(ItemsCommandService));

            return services;
        }

        //public static IServiceCollection AddCommonPipelineRequestPerformanceServices(this IServiceCollection services)
        //{
        //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformance<,>));

        //return services;
        //}

        //public static IServiceCollection AddCommonPipelineValidationServices(this IServiceCollection services)
        //{
        //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidation<,>));
        //services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        //return services;
        //}
    }
}