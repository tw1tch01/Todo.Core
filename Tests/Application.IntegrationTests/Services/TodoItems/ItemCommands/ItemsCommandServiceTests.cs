using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Todo.Application.IntegrationTests.TestingFactory;
using Todo.Application.Services.TodoItems.ItemCommands;
using Todo.Factories;

namespace Todo.Application.IntegrationTests.Services.TodoItems.ItemCommands
{
    [TestFixture]
    public class ItemsCommandServiceTests : MemorySetupFixture
    {
        private IItemsCommandService _commandService;

        [OneTimeSetUp]
        public void Init()
        {
            _commandService = _provider.GetRequiredService<IItemsCommandService>();
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
        public async Task CreateItem_IntegrationTest()
        {
            var itemDto = TodoItemFactory.GenerateCreateDto();

            var _ = await _commandService.CreateItem(itemDto);

            Assert.Pass();
        }
    }
}