using System.Threading.Tasks;
using AutoFixture;
using NUnit.Framework;
using Todo.Application.IntegrationTests.TestingFactory;
using Todo.Application.TodoItems.Commands.Update;
using Todo.DomainModels.TodoItems;

namespace Todo.Application.IntegrationTests.TodoItems.Commands.Update
{
    [TestFixture]
    public class UpdateItemRequestTests : MemorySetupFixture
    {
        private readonly Fixture _fixture = new Fixture();

        [Test]
        public async Task UpdateItemRequest()
        {
            var dto = new UpdateItemDto
            {
                Name = _fixture.Create<string>(),
                Description = _fixture.Create<string>()
            };
            var command = new UpdateItemRequest(_item.ItemId, dto);
            var _ = await _mediator.Send(command);

            Assert.Pass();
        }
    }
}