using System;
using System.Threading.Tasks;
using NUnit.Framework;
using Todo.Application.IntegrationTests.TestingFactory;
using Todo.Application.TodoItems.Queries.Get;

namespace Todo.Application.IntegrationTests.TodoItems.Queries.Get
{
    [TestFixture]
    public class GetItemsByParentIdTests : MemorySetupFixture
    {
        [Test]
        public async Task Handle_WhenNoEntitesAreFound_ReturnsEmptyList()
        {
            var query = new GetItemsByParentId(Guid.NewGuid());
            var _ = await _mediator.Send(query);

            Assert.Pass();
        }

        [Test]
        public async Task Handle_WhenEntitesAreFound_ReturnsCollectionOfItemDetails()
        {
            var query = new GetItemsByParentId(_itemWithChildren.ItemId);
            var _ = await _mediator.Send(query);

            Assert.Pass();
        }
    }
}