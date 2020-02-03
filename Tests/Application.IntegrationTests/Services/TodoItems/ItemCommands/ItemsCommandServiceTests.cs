using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Todo.Application.IntegrationTests.TestingFactory;
using Todo.Application.Services.TodoItems.ItemCommands;
using Todo.Domain.Entities;
using Todo.DomainModels.TodoItems;
using Todo.Factories;
using Todo.Services.TodoItems.Commands.CancelItem;
using Todo.Services.TodoItems.Commands.CompleteItem;
using Todo.Services.TodoItems.Commands.CreateItem;
using Todo.Services.TodoItems.Commands.DeleteItem;
using Todo.Services.TodoItems.Commands.ResetItem;
using Todo.Services.TodoItems.Commands.StartItem;
using Todo.Services.TodoItems.Commands.UpdateItem;

namespace Todo.Application.IntegrationTests.Services.TodoItems.ItemCommands
{
    [TestFixture]
    public class ItemsCommandServiceTests : MemorySetupFixture
    {
        private ItemsCommandService _commandService;

        [OneTimeSetUp]
        public void Init()
        {
            _commandService = new ItemsCommandService
            (
                _provider.GetRequiredService<ICreateItemService>(),
                _provider.GetRequiredService<IDeleteItemService>(),
                _provider.GetRequiredService<IUpdateItemService>(),
                _provider.GetRequiredService<ICancelItemService>(),
                _provider.GetRequiredService<ICompleteItemService>(),
                _provider.GetRequiredService<IResetItemService>(),
                _provider.GetRequiredService<IStartItemService>()
            );
        }

        [Test]
        public async Task AddChildItem_IntegrationTest()
        {
            var item = TodoItemFactory.GenerateItem();
            _memoryContext.Add(item);
            _memoryContext.SaveChanges();

            var childItemDto = TodoItemFactory.GenerateCreateDto();

            var _ = await _commandService.AddChildItem(item.ItemId, childItemDto);

            Assert.Pass();
        }

        [Test]
        public async Task CancelItem_IntegrationTest()
        {
            var item = new TodoItem();
            _memoryContext.Add(item);
            _memoryContext.SaveChanges();

            await _commandService.CancelItem(item.ItemId);

            Assert.Pass();
        }

        [Test]
        public async Task CompleteItem_IntegrationTest()
        {
            var item = new TodoItem();
            _memoryContext.Add(item);
            _memoryContext.SaveChanges();

            await _commandService.CompleteItem(item.ItemId);

            Assert.Pass();
        }

        [Test]
        public async Task CreateItem_IntegrationTest()
        {
            var itemDto = TodoItemFactory.GenerateCreateDto();

            var _ = await _commandService.CreateItem(itemDto);

            Assert.Pass();
        }

        [Test]
        public async Task DeleteItem_IntegrationTest()
        {
            var item = TodoItemFactory.GenerateItem();
            _memoryContext.Add(item);
            _memoryContext.SaveChanges();

            await _commandService.DeleteItem(item.ItemId);

            Assert.Pass();
        }

        [Test]
        public async Task ResetItem_IntegrationTest()
        {
            var item = new TodoItem();
            _memoryContext.Add(item);
            _memoryContext.SaveChanges();

            await _commandService.ResetItem(item.ItemId);

            Assert.Pass();
        }

        [Test]
        public async Task StartItem_IntegrationTest()
        {
            var item = new TodoItem();
            _memoryContext.Add(item);
            _memoryContext.SaveChanges();

            await _commandService.StartItem(item.ItemId);

            Assert.Pass();
        }

        [Test]
        public async Task UpdateItem_IntegrationTest()
        {
            var item = TodoItemFactory.GenerateItem();
            _memoryContext.Add(item);
            _memoryContext.SaveChanges();

            var itemDto = new UpdateItemDto
            {
                Description = "test"
            };

            await _commandService.UpdateItem(item.ItemId, itemDto);

            Assert.Pass();
        }
    }
}