using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Data.Repositories;
using Moq;
using NUnit.Framework;
using Todo.Application.Interfaces;
using Todo.Application.TodoItems.Queries.Get;
using Todo.Application.TodoItems.Specifications;
using Todo.Common.Exceptions;
using Todo.Domain.Entities;
using Todo.Factories;
using Todo.Models.TodoItems;

namespace Todo.Application.UnitTests.TodoItems.Queries.Get
{
    [TestFixture]
    public class GetItemByIdRequestTests
    {
        [Test]
        public void Handle_WhenItemDoesNotExist_ThrowsNotFoundException()
        {
            var mockRepository = new Mock<IContextRepository<ITodoContext>>();
            var mockMapper = new Mock<IMapper>();
            var query = new GetItemByIdRequest(Guid.NewGuid());
            var handler = new GetItemByIdRequest.RequestHandler(mockRepository.Object, mockMapper.Object);

            mockRepository.Setup(a => a.GetAsync(query.Specification)).ReturnsAsync((TodoItem)null);

            Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(query, default));
        }

        [Test]
        public async Task Handle_WhenItemExists_ReturnsItemDetails()
        {
            var item = TodoItemFactory.GenerateItem();
            var childItems = new List<TodoItem>();
            var details = TodoItemFactory.MappedItemDetails(item);

            var mockRepository = new Mock<IContextRepository<ITodoContext>>();
            var mockMapper = new Mock<IMapper>();

            var query = new GetItemByIdRequest(item.ItemId);
            var handler = new GetItemByIdRequest.RequestHandler(mockRepository.Object, mockMapper.Object);

            mockRepository.Setup(a => a.GetAsync(query.Specification)).ReturnsAsync(item);
            mockRepository.Setup(a => a.ListAsync(It.IsAny<GetItemsByParentId>())).ReturnsAsync(childItems);
            mockMapper.Setup(a => a.Map<TodoItemDetails>(item)).Returns(details);

            var result = await handler.Handle(query, default);

            Assert.AreEqual(details, result);
        }
    }
}