using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Todo.Application.IntegrationTests.TestingFactory;
using Todo.Application.Services.TodoItems;
using Todo.Domain.Enums;
using Todo.DomainModels.TodoItems;
using Todo.Factories;
using Todo.Services.TodoItems.Queries.GetItem;
using Todo.Services.TodoItems.Queries.Lookups.ChildItems;
using Todo.Services.TodoItems.Queries.Lookups.ParentItems;

namespace Todo.Application.IntegrationTests.Services.TodoItems.ItemQueries
{
    [TestFixture]
    public class ItemsQueryServiceTests : MemorySetupFixture
    {
        private ItemsQueryService _queryService;

        [OneTimeSetUp]
        public void Init()
        {
            _queryService = new ItemsQueryService
            (
                _provider.GetRequiredService<IGetItemService>(),
                _provider.GetRequiredService<IChildItemsLookupService>(),
                _provider.GetRequiredService<IParentItemsLookupService>()
            );
        }

        [Test]
        public async Task GetItem_IntegrationTest()
        {
            var item = TodoItemFactory.GenerateItemWithChildren(3);
            var note = TodoNoteFactory.GenerateNote(item.ItemId);
            var note2 = TodoNoteFactory.GenerateNote(item.ItemId);
            var reply = TodoNoteFactory.GenerateNote(item.ItemId);
            var reply2 = TodoNoteFactory.GenerateNote(item.ItemId);
            var reply3 = TodoNoteFactory.GenerateNote(item.ItemId);
            reply.Replies.Add(reply3);
            reply.Replies.Add(reply2);
            note.Replies.Add(reply);
            item.Notes.Add(note2);
            item.Notes.Add(note);
            _memoryContext.Add(item);
            _memoryContext.SaveChanges();

            var _ = await _queryService.GetItem(item.ItemId);

            Assert.Pass();
        }

        [Test]
        public async Task LookupChildItems_IntegrationTest()
        {
            var item = TodoItemFactory.GenerateItemWithChildren(3);
            _memoryContext.Add(item);
            _memoryContext.SaveChanges();

            var _ = await _queryService.LookupChildItems(item.ItemId);

            Assert.Pass();
        }

        [Test]
        public async Task LookupParentItems_IntegrationTest()
        {
            var item = TodoItemFactory.GenerateItem();
            item.CancelledOn = null;
            item.CompletedOn = null;
            item.StartedOn = null;
            item.DueDate = null;
            var parameters = new TodoItemLookupParams
            {
                FilterByStatus = TodoItemStatus.Pending
            };

            var _ = await _queryService.LookupParentItems(parameters);

            Assert.Pass();
        }

        [Test]
        public async Task PagedLookupParentItems_IntegrationTest()
        {
            var item = TodoItemFactory.GenerateItem();
            item.CancelledOn = null;
            item.CompletedOn = null;
            item.StartedOn = DateTime.UtcNow;
            item.DueDate = null;

            _memoryContext.Add(item);
            _memoryContext.SaveChanges();

            var page = 0;
            var pageSize = 10;
            var parameters = new TodoItemLookupParams
            {
            };

            var _ = await _queryService.PagedLookupParentItems(page, pageSize, parameters);

            Assert.Pass();
        }
    }
}