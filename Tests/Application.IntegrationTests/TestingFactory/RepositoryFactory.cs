using Data.Contexts;
using Data.Repositories;
using Microsoft.Extensions.Logging;
using Moq;
using Todo.Services.Common;

namespace Todo.Application.IntegrationTests.TestingFactory
{
    internal class RepositoryFactory
    {
        public static ContextRepository<ITodoContext> Create(ITodoContext context)
        {
            return new ContextRepository<ITodoContext>(context, new Mock<ILogger<IAuditedContext>>().Object);
        }
    }
}