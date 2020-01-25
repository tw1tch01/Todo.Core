using System;
using NUnit.Framework;
using Todo.Domain.Entities;
using Todo.Domain.Enums;
using Todo.Domain.Exceptions;

namespace Todo.Domain.UnitTests.HelperTests
{
    [TestFixture]
    public class TodoItemTests
    {
        #region GetStatus

        [Test]
        public void GetStatus_WhenTodoItemCompletedOnHasValue_ReturnsCompleted()
        {
            var item = new TodoItem
            {
                CompletedOn = DateTime.UtcNow,
                CancelledOn = null,
                DueDate = null,
                StartedOn = null
            };
            var status = item.GetStatus();
            Assert.AreEqual(TodoItemStatus.Completed, status);
        }

        [Test]
        public void GetStatus_WhenTodoItemCancelledOnHasValue_ReturnsCancelled()
        {
            var item = new TodoItem
            {
                CompletedOn = null,
                CancelledOn = DateTime.UtcNow,
                DueDate = null,
                StartedOn = null
            };
            var status = item.GetStatus();
            Assert.AreEqual(TodoItemStatus.Cancelled, status);
        }

        [Test]
        public void GetStatus_WhenTodoItemDueDateHasValueAndIsLessThanCurrentDate_ReturnsOverdue()
        {
            var item = new TodoItem
            {
                CompletedOn = null,
                CancelledOn = null,
                DueDate = DateTime.UtcNow.AddDays(-1),
                StartedOn = null
            };
            var status = item.GetStatus();
            Assert.AreEqual(TodoItemStatus.Overdue, status);
        }

        [Test]
        public void GetStatus_WhenTodoItemStartedOnHasValue_ReturnsInProcess()
        {
            var item = new TodoItem
            {
                CompletedOn = null,
                CancelledOn = null,
                DueDate = null,
                StartedOn = DateTime.UtcNow
            };
            var status = item.GetStatus();
            Assert.AreEqual(TodoItemStatus.InProgress, status);
        }

        [Test]
        public void GetStatus_WhenTodoItemIsBlank_ReturnsPending()
        {
            var item = new TodoItem
            {
                CompletedOn = null,
                CancelledOn = null,
                DueDate = null,
                StartedOn = null
            };
            var status = item.GetStatus();
            Assert.AreEqual(TodoItemStatus.Pending, status);
        }

        #endregion GetStatus

        #region CanBeCancelled

        [Test]
        public void CanBeCancelled_WhenCompletedOnIsNotNull_ReturnsFalse()
        {
            var item = new TodoItem
            {
                CompletedOn = DateTime.UtcNow,
                CancelledOn = null
            };
            var result = item.CanBeCancelled();
            Assert.IsFalse(result, "Should return false if item has been completed.");
        }

        [Test]
        public void CanBeCancelled_WhenCancelledOnIsNotNull_ReturnsFalse()
        {
            var item = new TodoItem
            {
                CompletedOn = null,
                CancelledOn = DateTime.UtcNow
            };
            var result = item.CanBeCancelled();
            Assert.IsFalse(result, "Should return false if item has been cancelled.");
        }

        [Test]
        public void CanBeCancelled_WhenCancelledOnAndCompletedOnAreNull_ReturnsTrue()
        {
            var item = new TodoItem
            {
                CompletedOn = null,
                CancelledOn = null
            };
            var result = item.CanBeCancelled();
            Assert.IsTrue(result, "Should return true if item hasn't been cancelled or completed.");
        }

        #endregion CanBeCancelled

        #region CancelItem

        [Test]
        public void CancelItem_WhenItemHasAlreadyBeenCancelled_ThrowsItemPreviouslyCancelledException()
        {
            var item = new TodoItem
            {
                CompletedOn = null,
                CancelledOn = DateTime.Now
            };
            var exception = Assert.Catch<ItemPreviouslyCancelledException>(() => item.CancelItem());
            Assert.AreEqual($"Item was previously cancelled on {item.CancelledOn}. (ItemId: {item.ItemId})", exception.Message);
        }

        [Test]
        public void CancelItem_WhenItemHasAlreadyBeenCompleted_ThrowsItemPreviouslyCompletedException()
        {
            var item = new TodoItem
            {
                CompletedOn = DateTime.Now,
                CancelledOn = null
            };
            var exception = Assert.Catch<ItemPreviouslyCompletedException>(() => item.CancelItem());
            Assert.AreEqual($"Item was previously completed on {item.CompletedOn}. (ItemId: {item.ItemId})", exception.Message);
        }

        [Test]
        public void CancelItem_WhenItemCanBeCancelled_SetsCancelledOn()
        {
            var item = new TodoItem
            {
                CompletedOn = null,
                CancelledOn = null
            };
            item.CancelItem();
            Assert.IsNotNull(item.CancelledOn);
        }

        #endregion CancelItem

        #region CanBeCompleted

        [Test]
        public void CanBeCompleted_WhenCompletedOnIsNotNull_ReturnsFalse()
        {
            var item = new TodoItem
            {
                CompletedOn = DateTime.UtcNow,
                CancelledOn = null
            };
            var result = item.CanBeCompleted();
            Assert.IsFalse(result, "Should return false if item has been completed.");
        }

        [Test]
        public void CanBeCompleted_WhenCancelledOnIsNotNull_ReturnsFalse()
        {
            var item = new TodoItem
            {
                CompletedOn = null,
                CancelledOn = DateTime.UtcNow
            };
            var result = item.CanBeCompleted();
            Assert.IsFalse(result, "Should return false if item has been cancelled.");
        }

        [Test]
        public void CanBeCompleted_WhenCancelledOnAndCompletedOnAreNull_ReturnsTrue()
        {
            var item = new TodoItem
            {
                CompletedOn = null,
                CancelledOn = null
            };
            var result = item.CanBeCompleted();
            Assert.IsTrue(result, "Should return true if item hasn't been cancelled or completed.");
        }

        #endregion CanBeCompleted

        #region CompleteItem

        [Test]
        public void CompleteItem_WhenItemHasAlreadyBeenCancelled_ThrowsInvalidOperationException()
        {
            var item = new TodoItem
            {
                CompletedOn = null,
                CancelledOn = DateTime.Now
            };
            var exception = Assert.Catch<ItemPreviouslyCancelledException>(() => item.CompleteItem());
            Assert.AreEqual($"Item was previously cancelled on {item.CancelledOn}. (ItemId: {item.ItemId})", exception.Message);
        }

        [Test]
        public void CompleteItem_WhenItemHasAlreadyBeenCompleted_ThrowsInvalidOperationException()
        {
            var item = new TodoItem
            {
                CompletedOn = DateTime.Now,
                CancelledOn = null
            };
            var exception = Assert.Catch<ItemPreviouslyCompletedException>(() => item.CompleteItem());
            Assert.AreEqual($"Item was previously completed on {item.CompletedOn}. (ItemId: {item.ItemId})", exception.Message);
        }

        [Test]
        public void CompleteItem_WhenItemCanBeCompleted_SetsCompletedOn()
        {
            var item = new TodoItem
            {
                CompletedOn = null,
                CancelledOn = null
            };
            item.CompleteItem();
            Assert.IsNotNull(item.CompletedOn);
        }

        #endregion CompleteItem

        #region StartItem

        [Test]
        public void StartItem_SetsProperties()
        {
            var item = new TodoItem
            {
                CompletedOn = null,
                CancelledOn = null,
                StartedOn = null
            };
            item.StartItem();
            Assert.Multiple(() =>
            {
                Assert.IsNotNull(item.StartedOn);
                Assert.IsNull(item.CompletedOn);
                Assert.IsNull(item.CancelledOn);
            });
        }

        #endregion StartItem

        #region ResetItem

        [Test]
        public void ResetItem_SetsProperties()
        {
            var item = new TodoItem
            {
                CompletedOn = null,
                CancelledOn = null,
                StartedOn = null
            };
            item.ResetItem();
            Assert.Multiple(() =>
            {
                Assert.IsNull(item.StartedOn);
                Assert.IsNull(item.CompletedOn);
                Assert.IsNull(item.CancelledOn);
            });
        }

        #endregion ResetItem

        #region IsParentItem

        [Test]
        public void IsParentItem_WhenParentItemIdIsNotNull_ReturnsFalse()
        {
            var item = new TodoItem
            {
                ParentItemId = Guid.NewGuid()
            };
            var result = item.IsParentItem();
            Assert.IsFalse(result);
        }

        [Test]
        public void IsParentItem_WhenParentItemIdAndParentItemAreNull_ReturnsTrue()
        {
            var item = new TodoItem
            {
                ParentItemId = null
            };
            var result = item.IsParentItem();
            Assert.IsTrue(result);
        }

        #endregion IsParentItem

        #region IsChildItem

        [Test]
        public void IsChildItem_WhenParentItemIdIsNull_ReturnsFalse()
        {
            var item = new TodoItem
            {
                ParentItemId = null
            };
            var result = item.IsChildItem();
            Assert.IsFalse(result);
        }

        [Test]
        public void IsChildItem_WhenParentItemIdIsNotNull_ReturnsTrue()
        {
            var item = new TodoItem
            {
                ParentItemId = Guid.NewGuid()
            };
            var result = item.IsChildItem();
            Assert.IsTrue(result);
        }

        #endregion IsChildItem
    }
}