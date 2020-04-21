using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Todo.Domain.Common;
using Todo.Domain.Enums;
using Todo.Domain.Exceptions;

[assembly: InternalsVisibleTo("Todo.Domain.UnitTests")]
namespace Todo.Domain.Entities
{
    public class TodoItem : BaseEntity
    {
        public TodoItem()
        {
            Rank = 0;
            ChildItems = new HashSet<TodoItem>();
            Notes = new HashSet<TodoItemNote>();
        }

        public Guid ItemId { get; set; }
        public Guid? ParentItemId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Rank { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? StartedOn { get; set; }
        public DateTime? CancelledOn { get; set; }
        public DateTime? CompletedOn { get; set; }

        #region Enumeration Properties

        public ImportanceLevel ImportanceLevel { get; set; }

        public PriorityLevel PriorityLevel { get; set; }

        #endregion Enumeration Properties

        #region Navigational Properties

        public TodoItem ParentItem { get; set; }
        public ICollection<TodoItem> ChildItems { get; private set; }
        public ICollection<TodoItemNote> Notes { get; private set; }

        #endregion Navigational Properties

        #region Methods

        public virtual void GroupNotesWithReplies()
        {
            Notes = TodoItemNote.GetParentNotes(Notes);
        }

        public virtual TodoItemStatus GetStatus()
        {
            if (CompletedOn.HasValue) return TodoItemStatus.Completed;

            if (CancelledOn.HasValue) return TodoItemStatus.Cancelled;

            if (DueDate < DateTime.UtcNow) return TodoItemStatus.Overdue;

            if (StartedOn.HasValue) return TodoItemStatus.InProgress;

            return TodoItemStatus.Pending;
        }

        public virtual bool IsCompleted()
        {
            return CompletedOn.HasValue;
        }

        public virtual bool IsCancelled()
        {
            return CancelledOn.HasValue;
        }

        public virtual bool HasStarted()
        {
            return StartedOn.HasValue;
        }

        public virtual void CancelItem()
        {
            if (IsCancelled()) throw new ItemPreviouslyCancelledException(CancelledOn.Value, ItemId);

            if (IsCompleted()) throw new ItemPreviouslyCompletedException(CompletedOn.Value, ItemId);

            ChildItems.OrderBy(item => item.Rank).ToList().ForEach(item =>
            {
                if (item.CanBeCancelled()) item.CancelItem();
            });
            CancelledOn = DateTime.UtcNow;
        }

        public virtual void CompleteItem()
        {
            if (CancelledOn.HasValue) throw new ItemPreviouslyCancelledException(CancelledOn.Value, ItemId);

            if (CompletedOn.HasValue) throw new ItemPreviouslyCompletedException(CompletedOn.Value, ItemId);

            ChildItems.OrderBy(item => item.Rank).ToList().ForEach(item =>
            {
                if (item.CanBeCompleted()) item.CompleteItem();
            });
            CompletedOn = DateTime.UtcNow;
        }

        public virtual void StartItem()
        {
            if (IsCancelled()) throw new ItemPreviouslyCancelledException(CancelledOn.Value, ItemId);

            if (IsCompleted()) throw new ItemPreviouslyCompletedException(CompletedOn.Value, ItemId);

            if (HasStarted()) throw new ItemAlreadyStartedException(StartedOn.Value, ItemId);

            StartedOn = DateTime.UtcNow;
            CompletedOn = null;
            CancelledOn = null;
        }

        public virtual void ResetItem()
        {
            StartedOn = null;
            CompletedOn = null;
            CancelledOn = null;
            ChildItems.ToList().ForEach(item => item.ResetItem());
        }

        public virtual bool IsParentItem()
        {
            return !ParentItemId.HasValue;
        }

        public virtual bool IsChildItem()
        {
            return ParentItemId.HasValue;
        }

        #endregion Methods

        #region Private methods

        internal bool CanBeCancelled()
        {
            return !IsCancelled() && !IsCompleted();
        }

        internal bool CanBeCompleted()
        {
            return !IsCancelled() && !IsCompleted();
        }

        #endregion Private methods
    }
}