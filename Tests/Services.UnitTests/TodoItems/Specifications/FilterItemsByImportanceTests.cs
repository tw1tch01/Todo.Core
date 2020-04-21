using NUnit.Framework;
using Todo.Domain.Entities;
using Todo.Domain.Enums;
using Todo.Services.TodoItems.Specifications;

namespace Todo.Services.UnitTests.TodoItems.Specifications
{
    [TestFixture]
    public class FilterItemsByImportanceTests
    {
        [Test]
        public void IsSatisfiedBy_WhenFilteringForTrivialItems_WhenItemIsTrivial_ReturnsTrue()
        {
            var item = new TodoItem
            {
                ImportanceLevel = ImportanceLevel.Trivial
            };
            var filterBy = new FilterItemsByImportance(ImportanceLevel.Trivial);
            var satisfied = filterBy.IsSatisfiedBy(item);
            Assert.IsTrue(satisfied);
        }

        [TestCase(ImportanceLevel.Minor)]
        [TestCase(ImportanceLevel.Major)]
        [TestCase(ImportanceLevel.Critical)]
        public void IsSatisfiedBy_WhenFilteringForTrivialItems_WhenItemIsNotTrivial_ReturnsFalse(ImportanceLevel importance)
        {
            var item = new TodoItem
            {
                ImportanceLevel = importance
            };
            var filterBy = new FilterItemsByImportance(ImportanceLevel.Trivial);
            var satisfied = filterBy.IsSatisfiedBy(item);
            Assert.IsFalse(satisfied);
        }

        [Test]
        public void IsSatisfiedBy_WhenFilteringForMinorItems_WhenItemIsMinor_ReturnsTrue()
        {
            var item = new TodoItem
            {
                ImportanceLevel = ImportanceLevel.Minor
            };
            var filterBy = new FilterItemsByImportance(ImportanceLevel.Minor);
            var satisfied = filterBy.IsSatisfiedBy(item);
            Assert.IsTrue(satisfied);
        }

        [TestCase(ImportanceLevel.Trivial)]
        [TestCase(ImportanceLevel.Major)]
        [TestCase(ImportanceLevel.Critical)]
        public void IsSatisfiedBy_WhenFilteringForMinorItems_WhenItemIsNotMinor_ReturnsFalse(ImportanceLevel importance)
        {
            var item = new TodoItem
            {
                ImportanceLevel = importance
            };
            var filterBy = new FilterItemsByImportance(ImportanceLevel.Minor);
            var satisfied = filterBy.IsSatisfiedBy(item);
            Assert.IsFalse(satisfied);
        }

        [Test]
        public void IsSatisfiedBy_WhenFilteringForMajorItems_WhenItemIsMajor_ReturnsTrue()
        {
            var item = new TodoItem
            {
                ImportanceLevel = ImportanceLevel.Major
            };
            var filterBy = new FilterItemsByImportance(ImportanceLevel.Major);
            var satisfied = filterBy.IsSatisfiedBy(item);
            Assert.IsTrue(satisfied);
        }

        [TestCase(ImportanceLevel.Trivial)]
        [TestCase(ImportanceLevel.Minor)]
        [TestCase(ImportanceLevel.Critical)]
        public void IsSatisfiedBy_WhenFilteringForMajorItems_WhenItemIsNotMajor_ReturnsFalse(ImportanceLevel importance)
        {
            var item = new TodoItem
            {
                ImportanceLevel = importance
            };
            var filterBy = new FilterItemsByImportance(ImportanceLevel.Major);
            var satisfied = filterBy.IsSatisfiedBy(item);
            Assert.IsFalse(satisfied);
        }

        [Test]
        public void IsSatisfiedBy_WhenFilteringForCriticalItems_WhenItemIsCritical_ReturnsTrue()
        {
            var item = new TodoItem
            {
                ImportanceLevel = ImportanceLevel.Critical
            };
            var filterBy = new FilterItemsByImportance(ImportanceLevel.Critical);
            var satisfied = filterBy.IsSatisfiedBy(item);
            Assert.IsTrue(satisfied);
        }

        [TestCase(ImportanceLevel.Trivial)]
        [TestCase(ImportanceLevel.Minor)]
        [TestCase(ImportanceLevel.Major)]
        public void IsSatisfiedBy_WhenFilteringForCriticalItems_WhenItemIsNotCritical_ReturnsFalse(ImportanceLevel importance)
        {
            var item = new TodoItem
            {
                ImportanceLevel = importance
            };
            var filterBy = new FilterItemsByImportance(ImportanceLevel.Critical);
            var satisfied = filterBy.IsSatisfiedBy(item);
            Assert.IsFalse(satisfied);
        }
    }
}