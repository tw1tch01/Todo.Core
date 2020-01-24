using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Data.Repositories;
using Moq;
using NUnit.Framework;
using Todo.Application.Interfaces;
using Todo.Application.TodoItems.Queries.Lookup;
using Todo.Domain.Entities;
using Todo.Factories;
using Todo.Models.TodoItems;

namespace Todo.Application.UnitTests.TodoItems.Queries.Get
{
    [TestFixture]
    public class GetItemsByParentIdRequestTests
    {
        [Test]
        public async Task Handle_WhenNoEntitesAreFound_ReturnsEmptyList()
        {
            var mockRepository = new Mock<IContextRepository<ITodoContext>>();
            var mockMapper = new Mock<IMapper>();
            var query = new ChildItemsLookupRequest(Guid.NewGuid());
            var handler = new ChildItemsLookupRequest.RequestHandler(mockRepository.Object, mockMapper.Object);

            ICollection<TodoItem> items = new List<TodoItem>();
            ICollection<TodoItemLookup> details = new List<TodoItemLookup>();

            mockRepository.Setup(a => a.ListAsync(query.Specification)).Returns(Task.FromResult(items));
            mockMapper.Setup(a => a.Map<ICollection<TodoItemLookup>>(items)).Returns(details);

            var result = await handler.Handle(query, default);
            Assert.Multiple(() =>
            {
                Assert.IsNotNull(result);
                Assert.IsEmpty(result);
                Assert.IsInstanceOf<ICollection<TodoItemLookup>>(result);
            });
        }

        [Test]
        public async Task Handle_WhenEntitesAreFound_ReturnsCollectionOfItemDetails()
        {
            var mockRepository = new Mock<IContextRepository<ITodoContext>>();
            var mockMapper = new Mock<IMapper>();
            var query = new ChildItemsLookupRequest(Guid.NewGuid());
            var handler = new ChildItemsLookupRequest.RequestHandler(mockRepository.Object, mockMapper.Object);

            ICollection<TodoItem> items = new List<TodoItem> { TodoItemFactory.GenerateItem() };
            ICollection<TodoItemLookup> details = items.Select(TodoItemFactory.MappedItemLookup).ToList();

            mockRepository.Setup(a => a.ListAsync(query.Specification)).Returns(Task.FromResult(items));
            mockMapper.Setup(a => a.Map<ICollection<TodoItemLookup>>(items)).Returns(details);

            var result = await handler.Handle(query, default);
            Assert.Multiple(() =>
            {
                Assert.IsNotNull(result);
                Assert.AreEqual(items.Count, result.Count);
                Assert.IsInstanceOf<ICollection<TodoItemLookup>>(result);
            });
        }
    }
}