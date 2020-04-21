using NUnit.Framework;
using Todo.Services.TodoItems.Queries.Lookups.ParentItems;

namespace Todo.Services.UnitTests.TodoItems.Queries.Lookups
{
    [TestFixture]
    public class ParentItemsLookupServiceTests
    {
        #region ValidatePaging

        [Test]
        public void ValidatePaging_WhenPageIsLessThanZero_SetsPageToZero()
        {
            int page = -1;

            var result = ParentItemsLookupService.ValidatePaging(page, 10);

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
            var result = ParentItemsLookupService.ValidatePaging(page, 10);

            Assert.AreEqual(page, result.page);
        }

        [TestCase(0)]
        [TestCase(-1)]
        public void ValidatePaging_WhenPageSizeIsLessThanOrEqualToZero_SetsPageSizeEqualToDefaultPageSize(int pageSize)
        {
            var result = ParentItemsLookupService.ValidatePaging(0, pageSize);

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

            var result = ParentItemsLookupService.ValidatePaging(0, pageSize);

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

            var result = ParentItemsLookupService.ValidatePaging(0, pageSize);

            Assert.AreEqual(pageSize, result.pageSize);
        }

        #endregion ValidatePaging
    }
}