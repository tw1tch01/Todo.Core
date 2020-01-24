using System;
using System.Threading.Tasks;
using NUnit.Framework;
using Todo.Application.IntegrationTests.TestingFactory;
using Todo.Application.TodoItems.Queries.Lookup;

namespace Todo.Application.IntegrationTests.TodoItems.Queries.Lookup
{
    [TestFixture]
    public class PagedParentItemsLookupTests : MemorySetupFixture
    {
        [Test]
        public async Task PagedParentItems()
        {
            var query = new PagedParentItemsLookup(0, 25);
            var _ = await _mediator.Send(query);

            Assert.Pass();
        }

        [Test]
        public async Task PagedParentItems_CreatedAfterDate()
        {
            var createdAfter = DateTime.UtcNow;
            var query = new PagedParentItemsLookup(0, 25);
            query.CreatedAfter(createdAfter);

            var _ = await _mediator.Send(query);

            Assert.Pass();
        }

        [Test]
        public async Task PagedParentItems_CreatedBeforeDate()
        {
            var createdBefore = DateTime.UtcNow;
            var query = new PagedParentItemsLookup(0, 25);
            query.CreatedBefore(createdBefore);

            var _ = await _mediator.Send(query);

            Assert.Pass();
        }
    }
}