﻿using System;
using NUnit.Framework;
using Todo.Domain.Entities;
using Todo.Domain.Enums;
using Todo.Domain.Exceptions;

namespace Todo.Domain.UnitTests.Entities
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
                CancelledOn = DateTime.UtcNow
            };
            Assert.Throws<ItemPreviouslyCancelledException>(() => item.CancelItem());
        }

        [Test]
        public void CancelItem_WhenItemHasAlreadyBeenCompleted_ThrowsItemPreviouslyCompletedException()
        {
            var item = new TodoItem
            {
                CompletedOn = DateTime.UtcNow,
                CancelledOn = null
            };
            Assert.Throws<ItemPreviouslyCompletedException>(() => item.CancelItem());
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

        #region IsCancelled

        [Test]
        public void IsCancelled_WhenCancelledOnHasValue_ReturnsTrue()
        {
            var item = new TodoItem
            {
                CancelledOn = DateTime.UtcNow
            };
            var result = item.IsCancelled();
            Assert.IsTrue(result, "Should return true if CancelledOn has a value");
        }

        [Test]
        public void IsCancelled_WhenCancelledOnIsNull_ReturnsFalse()
        {
            var item = new TodoItem
            {
                CancelledOn = null
            };
            var result = item.IsCancelled();
            Assert.IsFalse(result, "Should return false if CancelledOn does not have a value");
        }

        #endregion IsCancelled

        #region IsCompleted

        [Test]
        public void IsCompleted_WhenCompletedOnHasValue_ReturnsTrue()
        {
            var item = new TodoItem
            {
                CompletedOn = DateTime.UtcNow
            };
            var result = item.IsCompleted();
            Assert.IsTrue(result, "Should return true if CompletedOn has a value");
        }

        [Test]
        public void IsCompleted_WhenCompletedOnIsNull_ReturnsFalse()
        {
            var item = new TodoItem
            {
                CompletedOn = null
            };
            var result = item.IsCompleted();
            Assert.IsFalse(result, "Should return false if CompletedOn does not have a value");
        }

        #endregion IsCompleted

        #region HasStarted

        [Test]
        public void HasStarted_WhenStartedOnHasValue_ReturnsTrue()
        {
            var item = new TodoItem
            {
                StartedOn = DateTime.UtcNow
            };
            var result = item.HasStarted();
            Assert.IsTrue(result, "Should return true if StartedOn has a value");
        }

        [Test]
        public void HasStarted_WhenStartedOnIsNull_ReturnsFalse()
        {
            var item = new TodoItem
            {
                StartedOn = null
            };
            var result = item.HasStarted();
            Assert.IsFalse(result, "Should return false if StartedOn does not have a value");
        }

        #endregion HasStarted

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
        public void CompleteItem_WhenItemHasAlreadyBeenCancelled_ThrowsItemPreviouslyCancelledException()
        {
            var item = new TodoItem
            {
                CompletedOn = null,
                CancelledOn = DateTime.UtcNow
            };
            Assert.Throws<ItemPreviouslyCancelledException>(() => item.CompleteItem());
        }

        [Test]
        public void CompleteItem_WhenItemHasAlreadyBeenCompleted_ThrowsItemPreviouslyCompletedException()
        {
            var item = new TodoItem
            {
                CompletedOn = DateTime.UtcNow,
                CancelledOn = null
            };
            Assert.Throws<ItemPreviouslyCompletedException>(() => item.CompleteItem());
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
        public void StartItem_WhenItemAlreadyStarted_ThrowsItemAlreadyStartedException()
        {
            var item = new TodoItem
            {
                StartedOn = DateTime.UtcNow,
                CancelledOn = null,
                CompletedOn = null
            };
            Assert.Throws<ItemAlreadyStartedException>(() => item.StartItem());
        }

        [Test]
        public void StartItem_WhenItemHasAlreadyBeenCancelled_ThrowsItemPreviouslyCancelledException()
        {
            var item = new TodoItem
            {
                StartedOn = null,
                CancelledOn = DateTime.UtcNow,
                CompletedOn = null
            };
            Assert.Throws<ItemPreviouslyCancelledException>(() => item.StartItem());
        }

        [Test]
        public void StartItem_WhenItemHasAlreadyBeenCompleted_ThrowsItemPreviouslyCompletedException()
        {
            var item = new TodoItem
            {
                StartedOn = null,
                CancelledOn = null,
                CompletedOn = DateTime.UtcNow
            };
            Assert.Throws<ItemPreviouslyCompletedException>(() => item.StartItem());
        }

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