using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Todo.DomainModels.Mappings;
using Todo.Services.Common;

namespace Todo.Application.IntegrationTests.TestingFactory
{
    [SetUpFixture]
    public class MemorySetupFixture
    {
        protected readonly ServiceProvider _provider;

        protected readonly MemoryContext _memoryContext;

        public MemorySetupFixture()
        {
            var services = RegisterDependencies();
            _provider = services.BuildServiceProvider();
            _memoryContext = _provider.GetRequiredService<MemoryContext>();
            _memoryContext.Seed();
        }

        private static ServiceCollection RegisterDependencies()
        {
            var services = new ServiceCollection();

            services.AddDbContext<MemoryContext>(opt => opt.UseInMemoryDatabase(Guid.NewGuid().ToString()));
            services.AddTransient(typeof(ITodoContext), typeof(MemoryContext));
            services.AddApplication();

            services.AddSingleton(opt => new MapperConfiguration(config =>
            {
                config.AddProfile<MappingProfile>();
            }).CreateMapper());

            return services;
        }
    }
}