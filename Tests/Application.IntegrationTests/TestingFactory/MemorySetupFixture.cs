using System;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
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
            //_memoryContext.Seed();
        }

        private static ServiceCollection RegisterDependencies()
        {
            var services = new ServiceCollection();

            //services.AddDbContext<MemoryContext>(opt => opt
            //    .UseMySql($"Server=localhost;Database=todoapplication;User=root;Password=root;",
            //    contextOptions =>
            //    {
            //        contextOptions.ServerVersion(new Version(8, 0, 16), ServerType.MySql);
            //        contextOptions.MigrationsAssembly("Todo.Persistence.MySQL");
            //    }), ServiceLifetime.Transient);
            services.AddDbContext<MemoryContext>(opt => opt.UseInMemoryDatabase(Guid.NewGuid().ToString()), ServiceLifetime.Transient);
            services.AddTransient<ITodoContext, MemoryContext>();
            services.AddApplication();

            return services;
        }
    }
}