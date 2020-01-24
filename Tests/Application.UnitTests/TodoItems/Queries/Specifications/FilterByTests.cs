using Moq;
using NUnit.Framework;
using Todo.Application.TodoItems.Queries.Specifications;
using Todo.Domain.Entities;
using Todo.Domain.Enums;
using Todo.Models.TodoItems.Enums;

namespace Todo.Application.UnitTests.TodoItems.Queries.Specifications
{
    [TestFixture]
    public class FilterByTests
    {
        #region Status

        [Test]
        public void IsSatisfiedBy_WhenFilteringForPendingItems_WhenItemIsPending_ReturnsTrue()
        {
            var mockItem = new Mock<TodoItem>();
            mockItem.Setup(a => a.GetStatus()).Returns(TodoItemStatus.Pending);
            var filterBy = new FilterItemsBy.Status(FilterTodoItemsBy.Status.Pending);
            var satisfied = filterBy.IsSatisfiedBy(mockItem.Object);
            Assert.IsTrue(satisfied);
        }

        [TestCase(TodoItemStatus.Cancelled)]
        [TestCase(TodoItemStatus.Completed)]
        [TestCase(TodoItemStatus.InProgress)]
        [TestCase(TodoItemStatus.Overdue)]
        public void IsSatisfiedBy_WhenFilteringForPendingItems_WhenItemIsNotPending_ReturnsFalse(TodoItemStatus status)
        {
            var mockItem = new Mock<TodoItem>();
            mockItem.Setup(a => a.GetStatus()).Returns(status);
            var filterBy = new FilterItemsBy.Status(FilterTodoItemsBy.Status.Pending);
            var satisfied = filterBy.IsSatisfiedBy(mockItem.Object);
            Assert.IsFalse(satisfied);
        }

        [Test]
        public void IsSatisfiedBy_WhenFilteringForCompletedItems_WhenItemIsCompleted_ReturnsTrue()
        {
            var mockItem = new Mock<TodoItem>();
            mockItem.Setup(a => a.GetStatus()).Returns(TodoItemStatus.Completed);
            var filterBy = new FilterItemsBy.Status(FilterTodoItemsBy.Status.Completed);
            var satisfied = filterBy.IsSatisfiedBy(mockItem.Object);
            Assert.IsTrue(satisfied);
        }

        [TestCase(TodoItemStatus.Pending)]
        [TestCase(TodoItemStatus.Cancelled)]
        [TestCase(TodoItemStatus.InProgress)]
        [TestCase(TodoItemStatus.Overdue)]
        public void IsSatisfiedBy_WhenFilteringForCompletedItems_WhenItemIsNotCompleted_ReturnsFalse(TodoItemStatus status)
        {
            var mockItem = new Mock<TodoItem>();
            mockItem.Setup(a => a.GetStatus()).Returns(status);
            var filterBy = new FilterItemsBy.Status(FilterTodoItemsBy.Status.Completed);
            var satisfied = filterBy.IsSatisfiedBy(mockItem.Object);
            Assert.IsFalse(satisfied);
        }

        [Test]
        public void IsSatisfiedBy_WhenFilteringForCancelledItems_WhenItemIsCancelled_ReturnsTrue()
        {
            var mockItem = new Mock<TodoItem>();
            mockItem.Setup(a => a.GetStatus()).Returns(TodoItemStatus.Cancelled);
            var filterBy = new FilterItemsBy.Status(FilterTodoItemsBy.Status.Cancelled);
            var satisfied = filterBy.IsSatisfiedBy(mockItem.Object);
            Assert.IsTrue(satisfied);
        }

        [TestCase(TodoItemStatus.Pending)]
        [TestCase(TodoItemStatus.Completed)]
        [TestCase(TodoItemStatus.InProgress)]
        [TestCase(TodoItemStatus.Overdue)]
        public void IsSatisfiedBy_WhenFilteringForCancelledItems_WhenItemIsNotCancelled_ReturnsFalse(TodoItemStatus status)
        {
            var mockItem = new Mock<TodoItem>();
            mockItem.Setup(a => a.GetStatus()).Returns(status);
            var filterBy = new FilterItemsBy.Status(FilterTodoItemsBy.Status.Cancelled);
            var satisfied = filterBy.IsSatisfiedBy(mockItem.Object);
            Assert.IsFalse(satisfied);
        }

        [Test]
        public void IsSatisfiedBy_WhenFilteringForInProgressItems_WhenItemIsInProgress_ReturnsTrue()
        {
            var mockItem = new Mock<TodoItem>();
            mockItem.Setup(a => a.GetStatus()).Returns(TodoItemStatus.InProgress);
            var filterBy = new FilterItemsBy.Status(FilterTodoItemsBy.Status.InProgress);
            var satisfied = filterBy.IsSatisfiedBy(mockItem.Object);
            Assert.IsTrue(satisfied);
        }

        [TestCase(TodoItemStatus.Pending)]
        [TestCase(TodoItemStatus.Completed)]
        [TestCase(TodoItemStatus.Cancelled)]
        [TestCase(TodoItemStatus.Overdue)]
        public void IsSatisfiedBy_WhenFilteringForInProgressItems_WhenItemIsNotInProgress_ReturnsFalse(TodoItemStatus status)
        {
            var mockItem = new Mock<TodoItem>();
            mockItem.Setup(a => a.GetStatus()).Returns(status);
            var filterBy = new FilterItemsBy.Status(FilterTodoItemsBy.Status.InProgress);
            var satisfied = filterBy.IsSatisfiedBy(mockItem.Object);
            Assert.IsFalse(satisfied);
        }

        [Test]
        public void IsSatisfiedBy_WhenFilteringForOverdueItems_WhenItemIsOverdue_ReturnsTrue()
        {
            var mockItem = new Mock<TodoItem>();
            mockItem.Setup(a => a.GetStatus()).Returns(TodoItemStatus.Overdue);
            var filterBy = new FilterItemsBy.Status(FilterTodoItemsBy.Status.Overdue);
            var satisfied = filterBy.IsSatisfiedBy(mockItem.Object);
            Assert.IsTrue(satisfied);
        }

        [TestCase(TodoItemStatus.Pending)]
        [TestCase(TodoItemStatus.Completed)]
        [TestCase(TodoItemStatus.Cancelled)]
        [TestCase(TodoItemStatus.InProgress)]
        public void IsSatisfiedBy_WhenFilteringForOverdueItems_WhenItemIsNotOverdue_ReturnsFalse(TodoItemStatus status)
        {
            var mockItem = new Mock<TodoItem>();
            mockItem.Setup(a => a.GetStatus()).Returns(status);
            var filterBy = new FilterItemsBy.Status(FilterTodoItemsBy.Status.Overdue);
            var satisfied = filterBy.IsSatisfiedBy(mockItem.Object);
            Assert.IsFalse(satisfied);
        }

        #endregion Status

        #region Importance

        [Test]
        public void IsSatisfiedBy_WhenFilteringForTrivialItems_WhenItemIsTrivial_ReturnsTrue()
        {
            var item = new TodoItem
            {
                ImportanceLevel = ImportanceLevel.Trivial
            };
            var filterBy = new FilterItemsBy.ImportanceLevel(FilterTodoItemsBy.Importance.Trivial);
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
            var filterBy = new FilterItemsBy.ImportanceLevel(FilterTodoItemsBy.Importance.Trivial);
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
            var filterBy = new FilterItemsBy.ImportanceLevel(FilterTodoItemsBy.Importance.Minor);
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
            var filterBy = new FilterItemsBy.ImportanceLevel(FilterTodoItemsBy.Importance.Minor);
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
            var filterBy = new FilterItemsBy.ImportanceLevel(FilterTodoItemsBy.Importance.Major);
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
            var filterBy = new FilterItemsBy.ImportanceLevel(FilterTodoItemsBy.Importance.Major);
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
            var filterBy = new FilterItemsBy.ImportanceLevel(FilterTodoItemsBy.Importance.Critical);
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
            var filterBy = new FilterItemsBy.ImportanceLevel(FilterTodoItemsBy.Importance.Critical);
            var satisfied = filterBy.IsSatisfiedBy(item);
            Assert.IsFalse(satisfied);
        }

        #endregion Importance

        #region Priority

        [Test]
        public void IsSatisfiedBy_WhenFilteringForLowestPriorityItems_WhenItemPriorityIsLowest_ReturnsTrue()
        {
            var item = new TodoItem
            {
                PriorityLevel = PriorityLevel.Lowest
            };
            var filterBy = new FilterItemsBy.PriortyLevel(FilterTodoItemsBy.Priority.Lowest);
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
            var filterBy = new FilterItemsBy.PriortyLevel(FilterTodoItemsBy.Priority.Lowest);
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
            var filterBy = new FilterItemsBy.PriortyLevel(FilterTodoItemsBy.Priority.Low);
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
            var filterBy = new FilterItemsBy.PriortyLevel(FilterTodoItemsBy.Priority.Low);
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
            var filterBy = new FilterItemsBy.PriortyLevel(FilterTodoItemsBy.Priority.Medium);
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
            var filterBy = new FilterItemsBy.PriortyLevel(FilterTodoItemsBy.Priority.Medium);
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
            var filterBy = new FilterItemsBy.PriortyLevel(FilterTodoItemsBy.Priority.High);
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
            var filterBy = new FilterItemsBy.PriortyLevel(FilterTodoItemsBy.Priority.High);
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
            var filterBy = new FilterItemsBy.PriortyLevel(FilterTodoItemsBy.Priority.Urgent);
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
            var filterBy = new FilterItemsBy.PriortyLevel(FilterTodoItemsBy.Priority.Urgent);
            var satisfied = filterBy.IsSatisfiedBy(item);
            Assert.IsFalse(satisfied);
        }

        #endregion Priority
    }
}