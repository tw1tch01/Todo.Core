using System;
using System.Linq;
using System.Threading.Tasks;
using Data.Repositories;
using Moq;
using NUnit.Framework;
using Todo.Application.Interfaces;
using Todo.Application.TodoItems.Commands.Actions;
using Todo.Application.TodoItems.Queries.Specifications;
using Todo.Common.Exceptions;
using Todo.Domain.Entities;

namespace Todo.Application.UnitTests.TodoItems.Commands.Actions
{
    [TestFixture]
    public class ResetItemRequestTests
    {
        [Test]
        public void Handle_WhenNoItemFound_ThrowsNotFoundException()
        {
            TodoItem item = null;
            var mockRepository = new Mock<IContextRepository<ITodoContext>>();
            mockRepository.Setup(m => m.GetAsync(It.IsAny<GetItemById>())).ReturnsAsync(() => item);

            var command = new ResetItemRequest(Guid.NewGuid());
            var handler = new ResetItemRequest.RequestHandler(mockRepository.Object);

            Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, default));
        }

        [Test]
        public async Task Handle_WhenItemExists_ResetsProperties()
        {
            var item = new TodoItem
            {
                StartedOn = DateTime.UtcNow,
                CancelledOn = DateTime.UtcNow,
                CompletedOn = DateTime.UtcNow
            };
            var mockRepository = new Mock<IContextRepository<ITodoContext>>();
            mockRepository.Setup(m => m.GetAsync(It.IsAny<GetItemById>())).ReturnsAsync(() => item);

            var command = new ResetItemRequest(Guid.NewGuid());
            var handler = new ResetItemRequest.RequestHandler(mockRepository.Object);

            await handler.Handle(command, default);

            Assert.Multiple(() =>
            {
                Assert.IsNull(item.StartedOn);
                Assert.IsNull(item.CancelledOn);
                Assert.IsNull(item.CompletedOn);
            });
        }

        [Test]
        public async Task Handle_WhenItemWithChildExists_ResetsAllProperties()
        {
            var parentItem = new TodoItem
            {
                StartedOn = DateTime.UtcNow,
                CancelledOn = DateTime.UtcNow,
                CompletedOn = DateTime.UtcNow
            };
            parentItem.ChildItems.Add(new TodoItem
            {
                StartedOn = DateTime.UtcNow,
                CancelledOn = DateTime.UtcNow,
                CompletedOn = DateTime.UtcNow
            });
            var mockRepository = new Mock<IContextRepository<ITodoContext>>();
            mockRepository.Setup(m => m.GetAsync(It.IsAny<GetItemById>())).ReturnsAsync(() => parentItem);

            var command = new ResetItemRequest(Guid.NewGuid());
            var handler = new ResetItemRequest.RequestHandler(mockRepository.Object);

            await handler.Handle(command, default);

            Assert.Multiple(() =>
            {
                Assert.IsNull(parentItem.StartedOn);
                Assert.IsNull(parentItem.CancelledOn);
                Assert.IsNull(parentItem.CompletedOn);
                Assert.That(parentItem.ChildItems.All(i => i.StartedOn == null));
                Assert.That(parentItem.ChildItems.All(i => i.CancelledOn == null));
                Assert.That(parentItem.ChildItems.All(i => i.CompletedOn == null));
            });
        }
    }
}