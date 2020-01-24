using System;
using System.Threading.Tasks;
using AutoMapper;
using Data.Repositories;
using Moq;
using NUnit.Framework;
using Todo.Application.Interfaces;
using Todo.Application.TodoItems.Queries.Get;
using Todo.Application.UnitTests.TestingFactories;
using Todo.Common.Exceptions;
using Todo.Domain.Entities;
using Todo.Models.TodoItems;

namespace Todo.Application.UnitTests.TodoItems.Queries.Get
{
    [TestFixture]
    public class GetItemByIdTests
    {
        #region Specification

        [Test]
        public void IsSatisfiedBy_WhenItemIdMatchesSpecId_ReturnsTrue()
        {
            var id = Guid.NewGuid();
            var item = new TodoItem { ItemId = id };
            var specification = new GetItemById(id);
            var satisfied = specification.IsSatisfiedBy(item);
            Assert.IsTrue(satisfied);
        }

        [Test]
        public void IsSatisfiedBy_WhenItemIdDoesNotMatchSpecId_ReturnsFalse()
        {
            var item = new TodoItem { ItemId = Guid.NewGuid() };
            var specification = new GetItemById(Guid.NewGuid());
            var satisfied = specification.IsSatisfiedBy(item);
            Assert.IsFalse(satisfied);
        }

        #endregion Specification

        #region Handler

        [Test]
        public void Handle_WhenItemDoesNotExist_ThrowsNotFoundException()
        {
            var mockRepository = new Mock<IContextRepository<ITodoContext>>();
            var mockMapper = new Mock<IMapper>();
            var query = new GetItemById(Guid.NewGuid());
            var handler = new GetItemById.Handler(mockRepository.Object, mockMapper.Object);

            mockRepository.Setup(a => a.GetAsync(query)).Returns(Task.FromResult<TodoItem>(null));

            Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(query, default));
        }

        [Test]
        public async Task Handle_WhenItemExists_ReturnsItemDetails()
        {
            var item = TodoItemFactory.GenerateItem();
            var details = TodoItemFactory.MappedItemDetails(item);
            var mockRepository = new Mock<IContextRepository<ITodoContext>>();
            var mockMapper = new Mock<IMapper>();
            var query = new GetItemById(item.ItemId);
            var handler = new GetItemById.Handler(mockRepository.Object, mockMapper.Object);

            mockRepository.Setup(a => a.GetAsync(query)).Returns(Task.FromResult(item));
            mockMapper.Setup(a => a.Map<TodoItemDetails>(item)).Returns(details);

            var result = await handler.Handle(query, default);

            Assert.AreEqual(details, result);
        }

        #endregion Handler
    }
}