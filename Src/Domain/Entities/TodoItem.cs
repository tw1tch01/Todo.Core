using System;
using System.Collections.Generic;
using System.Linq;
using Todo.Domain.Common;
using Todo.Domain.Enums;

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
        //public string ImportanceLevelEnum { get; set; }
        //public string PriorityLevelEnum { get; set; }

        #region Enumeration Properties

        public ImportanceLevel ImportanceLevel
        {
            get; //=> (ImportanceLevel)Enum.Parse(typeof(ImportanceLevel), ImportanceLevelEnum);
            set; //=> ImportanceLevelEnum = value.ToString();
        }

        public PriorityLevel PriorityLevel
        {
            get; //=> (PriorityLevel)Enum.Parse(typeof(PriorityLevel), PriorityLevelEnum);
            set; //=> PriorityLevelEnum = value.ToString();
        }

        #endregion Enumeration Properties

        #region Navigational Properties

        public TodoItem ParentItem { get; set; }
        public ICollection<TodoItem> ChildItems { get; }
        public ICollection<TodoItemNote> Notes { get; }

        #endregion Navigational Properties

        #region Methods

        public virtual TodoItemStatus GetStatus()
        {
            if (CompletedOn.HasValue) return TodoItemStatus.Completed;

            if (CancelledOn.HasValue) return TodoItemStatus.Cancelled;

            if (DueDate < DateTime.UtcNow) return TodoItemStatus.Overdue;

            if (StartedOn.HasValue) return TodoItemStatus.InProgress;

            return TodoItemStatus.Pending;
        }

        public bool CanBeCancelled()
        {
            if (CompletedOn.HasValue) return false;

            return !CancelledOn.HasValue;
        }

        public void CancelItem()
        {
            if (CompletedOn.HasValue) throw new InvalidOperationException($"Cannot cancel a Completed item. Completed On: {CompletedOn.Value}");

            if (CancelledOn.HasValue) throw new InvalidOperationException($"Item was already cancelled on {CancelledOn.Value}");

            ChildItems.OrderBy(item => item.Rank).ToList().ForEach(item =>
            {
                if (item.CanBeCancelled()) item.CancelItem();
            });
            CancelledOn = DateTime.UtcNow;
        }

        public bool CanBeCompleted()
        {
            if (CancelledOn.HasValue) return false;

            return !CompletedOn.HasValue;
        }

        public void CompleteItem()
        {
            if (CompletedOn.HasValue) throw new InvalidOperationException($"Item was already completed on {CompletedOn.Value}");

            if (CancelledOn.HasValue) throw new InvalidOperationException($"Cannot complete a Cancelled item. Cancelled On: {CancelledOn.Value}");

            ChildItems.OrderBy(item => item.Rank).ToList().ForEach(item =>
            {
                if (item.CanBeCompleted()) item.CompleteItem();
            });
            CompletedOn = DateTime.UtcNow;
        }

        public void StartItem()
        {
            StartedOn = DateTime.UtcNow;
            CompletedOn = null;
            CancelledOn = null;
            ChildItems.ToList().ForEach(item => item.StartItem());
        }

        public void ResetItem()
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

        public bool IsChildItem()
        {
            return ParentItemId.HasValue;
        }

        #endregion Methods
    }
}