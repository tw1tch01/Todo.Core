using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Todo.Application.IntegrationTests.TestingFactory;
using Todo.Application.Services.TodoItems.ItemQueries;
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
            //var note = TodoNoteFactory.GenerateNote(item.ItemId);
            //var reply = TodoNoteFactory.GenerateNote(item.ItemId);
            //note.Replies.Add(reply);
            //item.Notes.Add(note);
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
            var parameters = new TodoItemLookupParams();

            var _ = await _queryService.LookupParentItems(parameters);

            Assert.Pass();
        }

        [Test]
        public async Task PagedLookupParentItems_IntegrationTest()
        {
            var page = 0;
            var pageSize = 10;
            var parameters = new TodoItemLookupParams();

            var _ = await _queryService.PagedLookupParentItems(page, pageSize, parameters);

            Assert.Pass();
        }
    }
}