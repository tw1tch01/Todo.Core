using System;
using System.Linq;
using System.Threading.Tasks;
using Data.Repositories;
using Moq;
using NUnit.Framework;
using Todo.Domain.Entities;
using Todo.Domain.Exceptions;
using Todo.Services.Common;
using Todo.Services.Common.Exceptions;
using Todo.Services.TodoItems.Commands.Actions.CancelItem;
using Todo.Services.TodoItems.Specifications;

namespace Todo.Application.UnitTests.TodoItems.Commands.Actions
{
    [TestFixture]
    public class CancelItemRequestTests
    {
        [Test]
        public void Handle_WhenNoItemFound_ThrowsNotFoundException()
        {
            TodoItem item = null;
            var mockRepository = new Mock<IContextRepository<ITodoContext>>();
            mockRepository.Setup(m => m.GetAsync(It.IsAny<GetItemById>())).ReturnsAsync(() => item);

            var service = new CancelItemService(mockRepository.Object);
            Assert.ThrowsAsync<NotFoundException>(async () => await service.CancelItem(Guid.NewGuid()));
        }

        [Test]
        public void Handle_WhenItemIsCancelled_ThrowsItemPreviouslyCancelledException()
        {
            var item = new TodoItem
            {
                ItemId = Guid.NewGuid(),
                CancelledOn = DateTime.UtcNow
            };
            var mockRepository = new Mock<IContextRepository<ITodoContext>>();
            mockRepository.Setup(m => m.GetAsync(It.Is<GetItemById>(a => a.ItemId == item.ItemId))).ReturnsAsync(() => item);

            var service = new CancelItemService(mockRepository.Object);

            Assert.ThrowsAsync<ItemPreviouslyCancelledException>(async () => await service.CancelItem(item.ItemId));
        }

        //[Test]
        //public void Handle_WhenItemIsCompleted_ThrowsItemPreviouslyCompletedException()
        //{
        //    var item = new TodoItem
        //    {
        //        CompletedOn = DateTime.UtcNow
        //    };
        //    var mockRepository = new Mock<IContextRepository<ITodoContext>>();
        //    mockRepository.Setup(m => m.GetAsync(It.IsAny<GetItemById>())).ReturnsAsync(() => item);

        //    var command = new CancelItemRequest(Guid.NewGuid());
        //    var handler = new CancelItemRequest.RequestHandler(mockRepository.Object);

        //    Assert.ThrowsAsync<ItemPreviouslyCompletedException>(async () => await handler.Handle(command, default));
        //}

        //[Test]
        //public async Task Handle_WhenItemFound_SetsCancelledOn()
        //{
        //    var item = new TodoItem
        //    {
        //        CancelledOn = null
        //    };
        //    var mockRepository = new Mock<IContextRepository<ITodoContext>>();
        //    mockRepository.Setup(m => m.GetAsync(It.IsAny<GetItemById>())).ReturnsAsync(() => item);

        //    var command = new CancelItemRequest(Guid.NewGuid());
        //    var handler = new CancelItemRequest.RequestHandler(mockRepository.Object);

        //    await handler.Handle(command, default);

        //    Assert.IsNotNull(item.CancelledOn);
        //}

        //[Test]
        //public async Task Handle_WhenItemWithChildExists_CancelsAllCancellableItems()
        //{
        //    var parentItem = new TodoItem
        //    {
        //        CancelledOn = null
        //    };
        //    parentItem.ChildItems.Add(new TodoItem
        //    {
        //        CancelledOn = null
        //    });
        //    var mockRepository = new Mock<IContextRepository<ITodoContext>>();
        //    mockRepository.Setup(m => m.GetAsync(It.IsAny<GetItemById>())).ReturnsAsync(() => parentItem);

        //    var command = new CancelItemRequest(Guid.NewGuid());
        //    var handler = new CancelItemRequest.RequestHandler(mockRepository.Object);

        //    await handler.Handle(command, default);

        //    Assert.Multiple(() =>
        //    {
        //        Assert.IsNotNull(parentItem.CancelledOn);
        //        Assert.That(parentItem.ChildItems.All(i => i.CancelledOn.HasValue));
        //    });
        //}
    }
}