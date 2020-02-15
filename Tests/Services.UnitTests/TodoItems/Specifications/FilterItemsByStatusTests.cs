using System;
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

        #region Pending

        [Test]
        public void IsSatisfiedBy_WhenFilteringForPendingItems_WhenItemIsPending_ReturnsTrue()
        {
            var item = new TodoItem
            {
                CancelledOn = null,
                CompletedOn = null,
                StartedOn = null,
                DueDate = null
            };
            var filterBy = new FilterItemsByStatus(TodoItemStatus.Pending);
            var satisfied = filterBy.IsSatisfiedBy(item);
            Assert.IsTrue(satisfied);
        }

        [Test]
        public void IsSatisfiedBy_WhenFilteringForPendingItems_WhenItemIsCancelled_ReturnsFalse()
        {
            var item = new TodoItem
            {
                CancelledOn = DateTime.UtcNow,
                CompletedOn = null,
                StartedOn = null,
                DueDate = null
            };
            var filterBy = new FilterItemsByStatus(TodoItemStatus.Pending);
            var satisfied = filterBy.IsSatisfiedBy(item);
            Assert.IsFalse(satisfied);
        }

        [Test]
        public void IsSatisfiedBy_WhenFilteringForPendingItems_WhenItemIsCompleted_ReturnsFalse()
        {
            var item = new TodoItem
            {
                CancelledOn = null,
                CompletedOn = DateTime.UtcNow,
                StartedOn = null,
                DueDate = null
            };
            var filterBy = new FilterItemsByStatus(TodoItemStatus.Pending);
            var satisfied = filterBy.IsSatisfiedBy(item);
            Assert.IsFalse(satisfied);
        }

        [Test]
        public void IsSatisfiedBy_WhenFilteringForPendingItems_WhenItemIsInProgress_ReturnsFalse()
        {
            var item = new TodoItem
            {
                CancelledOn = null,
                CompletedOn = null,
                StartedOn = DateTime.UtcNow,
                DueDate = null
            };
            var filterBy = new FilterItemsByStatus(TodoItemStatus.Pending);
            var satisfied = filterBy.IsSatisfiedBy(item);
            Assert.IsFalse(satisfied);
        }

        [Test]
        public void IsSatisfiedBy_WhenFilteringForPendingItems_WhenItemIsOverDue_ReturnsFalse()
        {
            var dateTime = DateTime.UtcNow;
            dateTime.AddDays(-1);
            var item = new TodoItem
            {
                CancelledOn = null,
                CompletedOn = null,
                StartedOn = null,
                DueDate = dateTime
            };
            var filterBy = new FilterItemsByStatus(TodoItemStatus.Pending);
            var satisfied = filterBy.IsSatisfiedBy(item);
            Assert.IsFalse(satisfied);
        }

        #endregion Pending

        #region Completed

        [Test]
        public void IsSatisfiedBy_WhenFilteringForCompletedItems_WhenItemIsCompleted_ReturnsTrue()
        {
            var item = new TodoItem
            {
                CancelledOn = null,
                CompletedOn = DateTime.UtcNow,
                StartedOn = null,
                DueDate = null
            };
            var filterBy = new FilterItemsByStatus(TodoItemStatus.Completed);
            var satisfied = filterBy.IsSatisfiedBy(item);
            Assert.IsTrue(satisfied);
        }

        [Test]
        public void IsSatisfiedBy_WhenFilteringForCompletedItems_WhenItemIsPending_ReturnsFalse()
        {
            var item = new TodoItem
            {
                CancelledOn = null,
                CompletedOn = null,
                StartedOn = null,
                DueDate = null
            };
            var filterBy = new FilterItemsByStatus(TodoItemStatus.Completed);
            var satisfied = filterBy.IsSatisfiedBy(item);
            Assert.IsFalse(satisfied);
        }

        [Test]
        public void IsSatisfiedBy_WhenFilteringForCompletedItems_WhenItemIsCancelled_ReturnsFalse()
        {
            var item = new TodoItem
            {
                CancelledOn = DateTime.UtcNow,
                CompletedOn = null,
                StartedOn = null,
                DueDate = null
            };
            var filterBy = new FilterItemsByStatus(TodoItemStatus.Completed);
            var satisfied = filterBy.IsSatisfiedBy(item);
            Assert.IsFalse(satisfied);
        }

        [Test]
        public void IsSatisfiedBy_WhenFilteringForCompletedItems_WhenItemIsInProgress_ReturnsFalse()
        {
            var item = new TodoItem
            {
                CancelledOn = null,
                CompletedOn = null,
                StartedOn = DateTime.UtcNow,
                DueDate = null
            };
            var filterBy = new FilterItemsByStatus(TodoItemStatus.Completed);
            var satisfied = filterBy.IsSatisfiedBy(item);
            Assert.IsFalse(satisfied);
        }

        [Test]
        public void IsSatisfiedBy_WhenFilteringForCompletedItems_WhenItemIsOverdue_ReturnsFalse()
        {
            var dateTime = DateTime.UtcNow;
            dateTime.AddDays(-1);
            var item = new TodoItem
            {
                CancelledOn = null,
                CompletedOn = null,
                StartedOn = null,
                DueDate = dateTime
            };
            var filterBy = new FilterItemsByStatus(TodoItemStatus.Completed);
            var satisfied = filterBy.IsSatisfiedBy(item);
            Assert.IsFalse(satisfied);
        }

        #endregion Completed

        #region Cancelled

        [Test]
        public void IsSatisfiedBy_WhenFilteringForCancelledItems_WhenItemIsCancelled_ReturnsTrue()
        {
            var item = new TodoItem
            {
                CancelledOn = DateTime.UtcNow,
                CompletedOn = null,
                StartedOn = null,
                DueDate = null
            };
            var filterBy = new FilterItemsByStatus(TodoItemStatus.Cancelled);
            var satisfied = filterBy.IsSatisfiedBy(item);
            Assert.IsTrue(satisfied);
        }

        [Test]
        public void IsSatisfiedBy_WhenFilteringForCancelledItems_WhenItemIsPending_ReturnsFalse()
        {
            var item = new TodoItem
            {
                CancelledOn = null,
                CompletedOn = null,
                StartedOn = null,
                DueDate = null
            };
            var filterBy = new FilterItemsByStatus(TodoItemStatus.Cancelled);
            var satisfied = filterBy.IsSatisfiedBy(item);
            Assert.IsFalse(satisfied);
        }

        [Test]
        public void IsSatisfiedBy_WhenFilteringForCancelledItems_WhenItemIsCompleted_ReturnsFalse()
        {
            var item = new TodoItem
            {
                CancelledOn = null,
                CompletedOn = DateTime.UtcNow,
                StartedOn = null,
                DueDate = null
            };
            var filterBy = new FilterItemsByStatus(TodoItemStatus.Cancelled);
            var satisfied = filterBy.IsSatisfiedBy(item);
            Assert.IsFalse(satisfied);
        }

        [Test]
        public void IsSatisfiedBy_WhenFilteringForCancelledItems_WhenItemIsInProgress_ReturnsFalse()
        {
            var item = new TodoItem
            {
                CancelledOn = null,
                CompletedOn = null,
                StartedOn = DateTime.UtcNow,
                DueDate = null
            };
            var filterBy = new FilterItemsByStatus(TodoItemStatus.Completed);
            var satisfied = filterBy.IsSatisfiedBy(item);
            Assert.IsFalse(satisfied);
        }

        [Test]
        public void IsSatisfiedBy_WhenFilteringForCancelledItems_WhenItemIsOverdue_ReturnsFalse()
        {
            var dateTime = DateTime.UtcNow;
            dateTime.AddDays(-1);
            var item = new TodoItem
            {
                CancelledOn = null,
                CompletedOn = null,
                StartedOn = null,
                DueDate = dateTime
            };
            var filterBy = new FilterItemsByStatus(TodoItemStatus.Completed);
            var satisfied = filterBy.IsSatisfiedBy(item);
            Assert.IsFalse(satisfied);
        }

        #endregion Cancelled

        #region InProgress

        [Test]
        public void IsSatisfiedBy_WhenFilteringForInProgressItems_WhenItemIsCancelled_ReturnsTrue()
        {
            var item = new TodoItem
            {
                CancelledOn = null,
                CompletedOn = null,
                StartedOn = DateTime.UtcNow,
                DueDate = null
            };
            var filterBy = new FilterItemsByStatus(TodoItemStatus.InProgress);
            var satisfied = filterBy.IsSatisfiedBy(item);
            Assert.IsTrue(satisfied);
        }

        [Test]
        public void IsSatisfiedBy_WhenFilteringForInProgressItems_WhenItemIsPending_ReturnsFalse()
        {
            var item = new TodoItem
            {
                CancelledOn = null,
                CompletedOn = null,
                StartedOn = null,
                DueDate = null
            };
            var filterBy = new FilterItemsByStatus(TodoItemStatus.InProgress);
            var satisfied = filterBy.IsSatisfiedBy(item);
            Assert.IsFalse(satisfied);
        }

        [Test]
        public void IsSatisfiedBy_WhenFilteringForInProgressItems_WhenItemIsCompleted_ReturnsFalse()
        {
            var item = new TodoItem
            {
                CancelledOn = null,
                CompletedOn = DateTime.UtcNow,
                StartedOn = null,
                DueDate = null
            };
            var filterBy = new FilterItemsByStatus(TodoItemStatus.InProgress);
            var satisfied = filterBy.IsSatisfiedBy(item);
            Assert.IsFalse(satisfied);
        }

        [Test]
        public void IsSatisfiedBy_WhenFilteringForInProgressItems_WhenItemIsCancelled_ReturnsFalse()
        {
            var item = new TodoItem
            {
                CancelledOn = DateTime.UtcNow,
                CompletedOn = null,
                StartedOn = null,
                DueDate = null
            };
            var filterBy = new FilterItemsByStatus(TodoItemStatus.InProgress);
            var satisfied = filterBy.IsSatisfiedBy(item);
            Assert.IsFalse(satisfied);
        }

        [Test]
        public void IsSatisfiedBy_WhenFilteringForInProgressItems_WhenItemIsOverdue_ReturnsFalse()
        {
            var dateTime = DateTime.UtcNow;
            dateTime.AddDays(-1);
            var item = new TodoItem
            {
                CancelledOn = null,
                CompletedOn = null,
                StartedOn = null,
                DueDate = dateTime
            };
            var filterBy = new FilterItemsByStatus(TodoItemStatus.InProgress);
            var satisfied = filterBy.IsSatisfiedBy(item);
            Assert.IsFalse(satisfied);
        }

        #endregion InProgress

        #region Overdue

        [Test]
        public void IsSatisfiedBy_WhenFilteringForOverdueItems_WhenItemIsOverdue_ReturnsTrue()
        {
            var dateTime = DateTime.UtcNow;
            dateTime.AddDays(-1);
            var item = new TodoItem
            {
                CancelledOn = null,
                CompletedOn = null,
                StartedOn = null,
                DueDate = dateTime
            };
            var filterBy = new FilterItemsByStatus(TodoItemStatus.Overdue);
            var satisfied = filterBy.IsSatisfiedBy(item);
            Assert.IsTrue(satisfied);
        }

        [Test]
        public void IsSatisfiedBy_WhenFilteringForOverdueItems_WhenItemIsPending_ReturnsFalse()
        {
            var item = new TodoItem
            {
                CancelledOn = null,
                CompletedOn = null,
                StartedOn = null,
                DueDate = null
            };
            var filterBy = new FilterItemsByStatus(TodoItemStatus.Overdue);
            var satisfied = filterBy.IsSatisfiedBy(item);
            Assert.IsFalse(satisfied);
        }

        [Test]
        public void IsSatisfiedBy_WhenFilteringForOverdueItems_WhenItemIsCompleted_ReturnsFalse()
        {
            var item = new TodoItem
            {
                CancelledOn = null,
                CompletedOn = DateTime.UtcNow,
                StartedOn = null,
                DueDate = null
            };
            var filterBy = new FilterItemsByStatus(TodoItemStatus.Overdue);
            var satisfied = filterBy.IsSatisfiedBy(item);
            Assert.IsFalse(satisfied);
        }

        [Test]
        public void IsSatisfiedBy_WhenFilteringForOverdueItems_WhenItemIsCancelled_ReturnsFalse()
        {
            var item = new TodoItem
            {
                CancelledOn = DateTime.UtcNow,
                CompletedOn = null,
                StartedOn = null,
                DueDate = null
            };
            var filterBy = new FilterItemsByStatus(TodoItemStatus.Overdue);
            var satisfied = filterBy.IsSatisfiedBy(item);
            Assert.IsFalse(satisfied);
        }

        [Test]
        public void IsSatisfiedBy_WhenFilteringForOverdueItems_WhenItemIsInProgress_ReturnsFalse()
        {
            var item = new TodoItem
            {
                CancelledOn = null,
                CompletedOn = null,
                StartedOn = DateTime.UtcNow,
                DueDate = null
            };
            var filterBy = new FilterItemsByStatus(TodoItemStatus.Overdue);
            var satisfied = filterBy.IsSatisfiedBy(item);
            Assert.IsFalse(satisfied);
        }

        #endregion Overdue

        #endregion Status
    }
}