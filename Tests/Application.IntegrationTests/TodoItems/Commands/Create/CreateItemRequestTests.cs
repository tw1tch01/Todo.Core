using System.Threading.Tasks;
using NUnit.Framework;
using Todo.Application.IntegrationTests.TestingFactory;
using Todo.Application.TodoItems.Commands.Create;
using Todo.Factories;

namespace Todo.Application.IntegrationTests.TodoItems.Commands.Create
{
    [TestFixture]
    public class CreateItemRequestTests : MemorySetupFixture
    {
        [Test]
        public async Task CreateItemRequest()
        {
            var dto = TodoItemFactory.GenerateCreateDto();
            var command = new CreateItemRequest(dto);
            var _ = await _mediator.Send(command);

            Assert.Pass();
        }
    }
}