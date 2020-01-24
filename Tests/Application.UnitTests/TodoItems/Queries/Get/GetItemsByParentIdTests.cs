using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Data.Repositories;
using Moq;
using NUnit.Framework;
using Todo.Application.Interfaces;
using Todo.Application.TodoItems.Queries.Get;
using Todo.Application.UnitTests.TestingFactories;
using Todo.Domain.Entities;
using Todo.Models.TodoItems;

namespace Todo.Application.UnitTests.TodoItems.Queries.Get
{
    [TestFixture]
    public class GetItemsByParentIdTests
    {
        #region Specification

        [Test]
        public void IsSatisfiedBy_WhenParentItemIdIsNull_ReturnsFalse()
        {
            var item = new TodoItem
            {
                ParentItemId = null
            };
            var specification = new GetItemsByParentId(Guid.NewGuid());
            var satisfied = specification.IsSatisfiedBy(item);
            Assert.IsFalse(satisfied);
        }

        [Test]
        public void IsSatisfiedBy_WhenParentItemIdDoesNotMatchValuePassedIn_ReturnsFalse()
        {
            var item = new TodoItem
            {
                ParentItemId = Guid.NewGuid()
            };
            var specification = new GetItemsByParentId(Guid.NewGuid());
            var satisfied = specification.IsSatisfiedBy(item);
            Assert.IsFalse(satisfied);
        }

        [Test]
        public void IsSatisfiedBy_WhenParentItemIdMatchesValuePassedIn_ReturnsTrue()
        {
            var parentId = Guid.NewGuid();
            var item = new TodoItem
            {
                ParentItemId = parentId
            };
            var specification = new GetItemsByParentId(parentId);
            var satisfied = specification.IsSatisfiedBy(item);
            Assert.IsTrue(satisfied);
        }

        #endregion Specification

        #region Handler

        [Test]
        public async Task Handle_WhenNoEntitesAreFound_ReturnsEmptyList()
        {
            var mockRepository = new Mock<IContextRepository<ITodoContext>>();
            var mockMapper = new Mock<IMapper>();
            var query = new GetItemsByParentId(Guid.NewGuid());
            var handler = new GetItemsByParentId.Handler(mockRepository.Object, mockMapper.Object);

            ICollection<TodoItem> items = new List<TodoItem>();
            ICollection<TodoItemDetails> details = new List<TodoItemDetails>();

            mockRepository.Setup(a => a.ListAsync(query)).Returns(Task.FromResult(items));
            mockMapper.Setup(a => a.Map<ICollection<TodoItemDetails>>(items)).Returns(details);

            var result = await handler.Handle(query, default);
            Assert.Multiple(() =>
            {
                Assert.IsNotNull(result);
                Assert.IsEmpty(result);
                Assert.IsInstanceOf<ICollection<TodoItemDetails>>(result);
            });
        }

        [Test]
        public async Task Handle_WhenEntitesAreFound_ReturnsCollectionOfItemDetails()
        {
            var mockRepository = new Mock<IContextRepository<ITodoContext>>();
            var mockMapper = new Mock<IMapper>();
            var query = new GetItemsByParentId(Guid.NewGuid());
            var handler = new GetItemsByParentId.Handler(mockRepository.Object, mockMapper.Object);

            ICollection<TodoItem> items = new List<TodoItem> { TodoItemFactory.GenerateItem() };
            ICollection<TodoItemDetails> details = items.Select(TodoItemFactory.MappedItemDetails).ToList();

            mockRepository.Setup(a => a.ListAsync(query)).Returns(Task.FromResult(items));
            mockMapper.Setup(a => a.Map<ICollection<TodoItemDetails>>(items)).Returns(details);

            var result = await handler.Handle(query, default);
            Assert.Multiple(() =>
            {
                Assert.IsNotNull(result);
                Assert.AreEqual(items.Count, result.Count);
                Assert.IsInstanceOf<ICollection<TodoItemDetails>>(result);
            });
        }

        #endregion Handler
    }
}