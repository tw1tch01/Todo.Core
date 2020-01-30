using Moq;
using NUnit.Framework;
using Todo.Domain.Entities;
using Todo.Domain.Enums;
using Todo.Services.TodoItems.Specifications;

namespace Todo.Services.UnitTests.TodoItems.Specifications
{
    [TestFixture]
    public class FilterItemsByStatusTests
    {
        #region Status

        [Test]
        public void IsSatisfiedBy_WhenFilteringForPendingItems_WhenItemIsPending_ReturnsTrue()
        {
            var mockItem = new Mock<TodoItem>();
            mockItem.Setup(a => a.GetStatus()).Returns(TodoItemStatus.Pending);
            var filterBy = new FilterItemsByStatus(TodoItemStatus.Pending);
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
            var filterBy = new FilterItemsByStatus(TodoItemStatus.Pending);
            var satisfied = filterBy.IsSatisfiedBy(mockItem.Object);
            Assert.IsFalse(satisfied);
        }

        [Test]
        public void IsSatisfiedBy_WhenFilteringForCompletedItems_WhenItemIsCompleted_ReturnsTrue()
        {
            var mockItem = new Mock<TodoItem>();
            mockItem.Setup(a => a.GetStatus()).Returns(TodoItemStatus.Completed);
            var filterBy = new FilterItemsByStatus(TodoItemStatus.Completed);
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
            var filterBy = new FilterItemsByStatus(TodoItemStatus.Completed);
            var satisfied = filterBy.IsSatisfiedBy(mockItem.Object);
            Assert.IsFalse(satisfied);
        }

        [Test]
        public void IsSatisfiedBy_WhenFilteringForCancelledItems_WhenItemIsCancelled_ReturnsTrue()
        {
            var mockItem = new Mock<TodoItem>();
            mockItem.Setup(a => a.GetStatus()).Returns(TodoItemStatus.Cancelled);
            var filterBy = new FilterItemsByStatus(TodoItemStatus.Cancelled);
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
            var filterBy = new FilterItemsByStatus(TodoItemStatus.Cancelled);
            var satisfied = filterBy.IsSatisfiedBy(mockItem.Object);
            Assert.IsFalse(satisfied);
        }

        [Test]
        public void IsSatisfiedBy_WhenFilteringForInProgressItems_WhenItemIsInProgress_ReturnsTrue()
        {
            var mockItem = new Mock<TodoItem>();
            mockItem.Setup(a => a.GetStatus()).Returns(TodoItemStatus.InProgress);
            var filterBy = new FilterItemsByStatus(TodoItemStatus.InProgress);
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
            var filterBy = new FilterItemsByStatus(TodoItemStatus.InProgress);
            var satisfied = filterBy.IsSatisfiedBy(mockItem.Object);
            Assert.IsFalse(satisfied);
        }

        [Test]
        public void IsSatisfiedBy_WhenFilteringForOverdueItems_WhenItemIsOverdue_ReturnsTrue()
        {
            var mockItem = new Mock<TodoItem>();
            mockItem.Setup(a => a.GetStatus()).Returns(TodoItemStatus.Overdue);
            var filterBy = new FilterItemsByStatus(TodoItemStatus.Overdue);
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
            var filterBy = new FilterItemsByStatus(TodoItemStatus.Overdue);
            var satisfied = filterBy.IsSatisfiedBy(mockItem.Object);
            Assert.IsFalse(satisfied);
        }

        #endregion Status
    }
}