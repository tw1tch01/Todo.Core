using System;
using System.Threading.Tasks;
using NUnit.Framework;
using Todo.Application.IntegrationTests.TestingFactory;
using Todo.Application.TodoItems.Queries.Get;
using Todo.Common.Exceptions;

namespace Todo.Application.IntegrationTests.TodoItems.Queries.Get
{
    [TestFixture]
    public class GetItemByIdRequestTests : MemorySetupFixture
    {
        [Test]
        public void Handle_WhenItemDoesNotExist_ThrowsNotFoundException()
        {
            var query = new GetItemByIdRequest(Guid.NewGuid());
            var _ = Assert.CatchAsync<NotFoundException>(() => _mediator.Send(query));

            Assert.Pass();
        }

        [Test]
        public async Task Handle_WhenItemExists_ReturnsItemDetails()
        {
            var query = new GetItemByIdRequest(_item.ItemId);
            var _ = await _mediator.Send(query);

            Assert.Pass();
        }
    }
}