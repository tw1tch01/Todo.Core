using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Data.Common;
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
    public class PagedParentItemsLookupTests
    {
        #region Handler

        [Test]
        public async Task Handle_WhenNoEntitesAreFound_ReturnsEmptyPagedCollection()
        {
            (int page, int pageSize) = (0, 10);
            var mockRepository = new Mock<IContextRepository<ITodoContext>>();
            var mockMapper = new Mock<IMapper>();
            var query = new PagedParentItemsLookup(page, pageSize);
            var handler = new PagedParentItemsLookup.RequestHandler(mockRepository.Object, mockMapper.Object);

            ICollection<TodoItem> items = new List<TodoItem>();
            PagedCollection<TodoItem> pagedCollection = new PagedCollection<TodoItem>(page, pageSize, 0, items);
            ICollection<ParentTodoItemLookup> details = new List<ParentTodoItemLookup>();

            mockRepository.Setup(a => a.PagedListAsync(page, pageSize, It.IsAny<GetParentItems>(), b => b.ItemId)).Returns(Task.FromResult(pagedCollection));
            mockMapper.Setup(a => a.Map<ICollection<ParentTodoItemLookup>>(items)).Returns(details);

            var result = await handler.Handle(query, default);

            Assert.Multiple(() =>
            {
                Assert.IsNotNull(result);
                Assert.IsEmpty(result.Items);
                Assert.IsInstanceOf<PagedCollection<ParentTodoItemLookup>>(result);
            });
        }

        [Test]
        public async Task Handle_WhenEntitesAreFound_ReturnsPagedCollectionOfItemDetails()
        {
            (int page, int pageSize) = (0, 10);
            var mockRepository = new Mock<IContextRepository<ITodoContext>>();
            var mockMapper = new Mock<IMapper>();
            var query = new PagedParentItemsLookup(page, pageSize);
            var handler = new PagedParentItemsLookup.RequestHandler(mockRepository.Object, mockMapper.Object);

            ICollection<TodoItem> items = new List<TodoItem> { TodoItemFactory.GenerateItem() };
            PagedCollection<TodoItem> pagedCollection = new PagedCollection<TodoItem>(page, pageSize, 0, items);
            ICollection<ParentTodoItemLookup> details = items.Select(TodoItemFactory.MappedParentItemLookup).ToList();

            mockRepository.Setup(a => a.PagedListAsync(page, pageSize, It.IsAny<GetParentItems>(), b => b.ItemId)).Returns(Task.FromResult(pagedCollection));
            mockMapper.Setup(a => a.Map<ICollection<ParentTodoItemLookup>>(items)).Returns(details);

            var result = await handler.Handle(query, default);
            Assert.Multiple(() =>
            {
                Assert.IsNotNull(result);
                Assert.AreEqual(items.Count, result.Items.Count);
                Assert.IsInstanceOf<PagedCollection<ParentTodoItemLookup>>(result);
            });
        }

        #endregion Handler

        #region ValidatePaging

        [Test]
        public void ValidatePaging_WhenPageIsLessThanZero_SetsPageToZero()
        {
            int page = -1;

            var result = PagedParentItemsLookup.RequestHandler.ValidatePaging(page, 10);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(0, result.page);
                Assert.AreNotEqual(page, result.page);
            });
        }

        [TestCase(0)]
        [TestCase(11)]
        public void ValidatePaging_WhenPageIsGreaterThanOrEqualToZero_LeavesPageUnchanged(int page)
        {
            var result = PagedParentItemsLookup.RequestHandler.ValidatePaging(page, 10);

            Assert.AreEqual(page, result.page);
        }

        [TestCase(0)]
        [TestCase(-1)]
        public void ValidatePaging_WhenPageSizeIsLessThanOrEqualToZero_SetsPageSizeEqualToDefaultPageSize(int pageSize)
        {
            var result = PagedParentItemsLookup.RequestHandler.ValidatePaging(0, pageSize);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(10, result.pageSize);
                Assert.AreNotEqual(pageSize, result.pageSize);
            });
        }

        [Test]
        public void ValidatePaging_WhenPageSizeIsGreaterThanMaximumPageSize_SetsPageSizeEqualToMaximumPageSize()
        {
            var pageSize = 26;

            var result = PagedParentItemsLookup.RequestHandler.ValidatePaging(0, pageSize);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(25, result.pageSize);
                Assert.AreNotEqual(pageSize, result.pageSize);
            });
        }

        [Test]
        public void ValidatePaging_WhenPageSizeIsBetweenDefaultAndMaximumPageSize_LeavesPageSizeUnchanged()
        {
            var pageSize = 20;

            var result = PagedParentItemsLookup.RequestHandler.ValidatePaging(0, pageSize);

            Assert.AreEqual(pageSize, result.pageSize);
        }

        #endregion ValidatePaging
    }
}