using NUnit.Framework;
using Todo.Domain.Entities;
using Todo.Domain.Enums;
using Todo.Services.TodoItems.Specifications;

namespace Todo.Services.UnitTests.TodoItems.Specifications
{
    [TestFixture]
    public class FilterItemsByPriorityLevelTests
    {
        [Test]
        public void IsSatisfiedBy_WhenFilteringForLowestPriorityItems_WhenItemPriorityIsLowest_ReturnsTrue()
        {
            var item = new TodoItem
            {
                PriorityLevel = PriorityLevel.Lowest
            };
            var filterBy = new FilterItemsByPriortyLevel(PriorityLevel.Lowest);
            var satisfied = filterBy.IsSatisfiedBy(item);
            Assert.IsTrue(satisfied);
        }

        [TestCase(PriorityLevel.Low)]
        [TestCase(PriorityLevel.Medium)]
        [TestCase(PriorityLevel.High)]
        [TestCase(PriorityLevel.Urgent)]
        public void IsSatisfiedBy_WhenFilteringForLowestPriorityItems_WhenItemPriorityIsNotLowest_ReturnsTrue(PriorityLevel priority)
        {
            var item = new TodoItem
            {
                PriorityLevel = priority
            };
            var filterBy = new FilterItemsByPriortyLevel(PriorityLevel.Lowest);
            var satisfied = filterBy.IsSatisfiedBy(item);
            Assert.IsFalse(satisfied);
        }

        [Test]
        public void IsSatisfiedBy_WhenFilteringForLowPriorityItems_WhenItemPriorityIsLow_ReturnsTrue()
        {
            var item = new TodoItem
            {
                PriorityLevel = PriorityLevel.Low
            };
            var filterBy = new FilterItemsByPriortyLevel(PriorityLevel.Low);
            var satisfied = filterBy.IsSatisfiedBy(item);
            Assert.IsTrue(satisfied);
        }

        [TestCase(PriorityLevel.Lowest)]
        [TestCase(PriorityLevel.Medium)]
        [TestCase(PriorityLevel.High)]
        [TestCase(PriorityLevel.Urgent)]
        public void IsSatisfiedBy_WhenFilteringForLowPriorityItems_WhenItemPriorityIsNotLow_ReturnsTrue(PriorityLevel priority)
        {
            var item = new TodoItem
            {
                PriorityLevel = priority
            };
            var filterBy = new FilterItemsByPriortyLevel(PriorityLevel.Low);
            var satisfied = filterBy.IsSatisfiedBy(item);
            Assert.IsFalse(satisfied);
        }

        [Test]
        public void IsSatisfiedBy_WhenFilteringForMediumPriorityItems_WhenItemPriorityIsMedium_ReturnsTrue()
        {
            var item = new TodoItem
            {
                PriorityLevel = PriorityLevel.Medium
            };
            var filterBy = new FilterItemsByPriortyLevel(PriorityLevel.Medium);
            var satisfied = filterBy.IsSatisfiedBy(item);
            Assert.IsTrue(satisfied);
        }

        [TestCase(PriorityLevel.Lowest)]
        [TestCase(PriorityLevel.Low)]
        [TestCase(PriorityLevel.High)]
        [TestCase(PriorityLevel.Urgent)]
        public void IsSatisfiedBy_WhenFilteringForMediumPriorityItems_WhenItemPriorityIsNotMedium_ReturnsTrue(PriorityLevel priority)
        {
            var item = new TodoItem
            {
                PriorityLevel = priority
            };
            var filterBy = new FilterItemsByPriortyLevel(PriorityLevel.Medium);
            var satisfied = filterBy.IsSatisfiedBy(item);
            Assert.IsFalse(satisfied);
        }

        [Test]
        public void IsSatisfiedBy_WhenFilteringForHighPriorityItems_WhenItemPriorityIsHigh_ReturnsTrue()
        {
            var item = new TodoItem
            {
                PriorityLevel = PriorityLevel.High
            };
            var filterBy = new FilterItemsByPriortyLevel(PriorityLevel.High);
            var satisfied = filterBy.IsSatisfiedBy(item);
            Assert.IsTrue(satisfied);
        }

        [TestCase(PriorityLevel.Lowest)]
        [TestCase(PriorityLevel.Low)]
        [TestCase(PriorityLevel.Medium)]
        [TestCase(PriorityLevel.Urgent)]
        public void IsSatisfiedBy_WhenFilteringForHighPriorityItems_WhenItemPriorityIsNotHigh_ReturnsTrue(PriorityLevel priority)
        {
            var item = new TodoItem
            {
                PriorityLevel = priority
            };
            var filterBy = new FilterItemsByPriortyLevel(PriorityLevel.High);
            var satisfied = filterBy.IsSatisfiedBy(item);
            Assert.IsFalse(satisfied);
        }

        [Test]
        public void IsSatisfiedBy_WhenFilteringForUrgentPriorityItems_WhenItemPriorityIsUrgent_ReturnsTrue()
        {
            var item = new TodoItem
            {
                PriorityLevel = PriorityLevel.Urgent
            };
            var filterBy = new FilterItemsByPriortyLevel(PriorityLevel.Urgent);
            var satisfied = filterBy.IsSatisfiedBy(item);
            Assert.IsTrue(satisfied);
        }

        [TestCase(PriorityLevel.Lowest)]
        [TestCase(PriorityLevel.Low)]
        [TestCase(PriorityLevel.Medium)]
        [TestCase(PriorityLevel.High)]
        public void IsSatisfiedBy_WhenFilteringForUrgentPriorityItems_WhenItemPriorityIsNotUrgent_ReturnsTrue(PriorityLevel priority)
        {
            var item = new TodoItem
            {
                PriorityLevel = priority
            };
            var filterBy = new FilterItemsByPriortyLevel(PriorityLevel.Urgent);
            var satisfied = filterBy.IsSatisfiedBy(item);
            Assert.IsFalse(satisfied);
        }
    }
}