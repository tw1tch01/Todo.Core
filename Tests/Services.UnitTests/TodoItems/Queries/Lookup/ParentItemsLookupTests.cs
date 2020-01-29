using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Data.Repositories;
using Moq;
using NUnit.Framework;
using Todo.Application.Interfaces;
using Todo.Application.TodoItems.Queries.Lookup;
using Todo.Application.TodoItems.Specifications;
using Todo.Domain.Entities;
using Todo.Factories;
using Todo.DomainModels.TodoItems;

namespace Todo.Application.UnitTests.TodoItems.Queries.Lookup
{
    [TestFixture]
    public class ParentItemsLookupTests
    {
        #region Handler

        [Test]
        public async Task Handle_WhenNoEntitesAreFound_ReturnsEmptyList()
        {
            var mockRepository = new Mock<IContextRepository<ITodoContext>>();
            var mockMapper = new Mock<IMapper>();
            var query = new ParentItemsLookup();
            var handler = new ParentItemsLookup.RequestHandler(mockRepository.Object, mockMapper.Object);

            ICollection<TodoItem> items = new List<TodoItem>();
            ICollection<ParentTodoItemLookup> details = new List<ParentTodoItemLookup>();

            mockRepository.Setup(a => a.ListAsync(It.IsAny<GetParentItems>())).Returns(Task.FromResult(items));
            mockMapper.Setup(a => a.Map<ICollection<ParentTodoItemLookup>>(items)).Returns(details);

            var result = await handler.Handle(query, default);
            Assert.Multiple(() =>
            {
                Assert.IsNotNull(result);
                Assert.IsEmpty(result);
                Assert.IsInstanceOf<ICollection<ParentTodoItemLookup>>(result);
            });
        }

        [Test]
        public async Task Handle_WhenEntitesAreFound_ReturnsCollectionOfItemDetails()
        {
            var mockRepository = new Mock<IContextRepository<ITodoContext>>();
            var mockMapper = new Mock<IMapper>();
            var query = new ParentItemsLookup();
            var handler = new ParentItemsLookup.RequestHandler(mockRepository.Object, mockMapper.Object);

            ICollection<TodoItem> items = new List<TodoItem> { TodoItemFactory.GenerateItem() };
            ICollection<ParentTodoItemLookup> details = items.Select(TodoItemFactory.MappedParentItemLookup).ToList();

            mockRepository.Setup(a => a.ListAsync(It.IsAny<GetParentItems>())).Returns(Task.FromResult(items));
            mockMapper.Setup(a => a.Map<ICollection<ParentTodoItemLookup>>(items)).Returns(details);

            var result = await handler.Handle(query, default);
            Assert.Multiple(() =>
            {
                Assert.IsNotNull(result);
                Assert.AreEqual(items.Count, result.Count);
                Assert.IsInstanceOf<ICollection<ParentTodoItemLookup>>(result);
            });
        }

        #endregion Handler
    }
}