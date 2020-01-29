using System;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Todo.Application.Interfaces;
using Todo.Domain.Entities;
using Todo.Factories;
using Todo.DomainModels.Mappings;
using Todo.Application.Services.TodoItems.ItemCommands;

namespace Todo.Application.IntegrationTests.TestingFactory
{
    [SetUpFixture]
    public class MemorySetupFixture
    {
        protected readonly TodoItem _item = TodoItemFactory.GenerateItem();
        protected readonly TodoItem _itemWithChildren = TodoItemFactory.GenerateItemWithChildren(5);
        protected readonly IMediator _mediator;
        protected readonly IItemsCommandService _itemsCommandService;

        public MemorySetupFixture()
        {
            var services = RegisterDependencies();
            var provider = services.BuildServiceProvider();

            SetupContext(provider);
            _mediator = provider.GetRequiredService<IMediator>();
            _itemsCommandService = provider.GetRequiredService<IItemsCommandService>();
        }

        private static ServiceCollection RegisterDependencies()
        {
            var services = new ServiceCollection();

            services.AddApplication();
            //services.AddCommonPipelineRequestPerformanceServices();

            services.AddDbContext<MemoryContext>(opt => opt.UseInMemoryDatabase(Guid.NewGuid().ToString()));
            services.AddTransient(typeof(ITodoContext), typeof(MemoryContext));

            //services.AddScoped(typeof(IRequestHandler<GetEntityQuery, EntityDto>), typeof(GetEntityQuery.Handler));
            //services.AddScoped(typeof(IRequestHandler<LookupEntitiesQuery, Lookup<EntityDto>>), typeof(LookupEntitiesQuery.Handler));

            services.AddSingleton(opt => new MapperConfiguration(config =>
            {
                config.AddProfile<MappingProfile>();
            }).CreateMapper());

            return services;
        }

        private void SetupContext(ServiceProvider provider)
        {
            var context = provider.GetRequiredService<MemoryContext>();
            context.Add(_item);
            context.Add(_itemWithChildren);
            context.Seed();
        }
    }
}