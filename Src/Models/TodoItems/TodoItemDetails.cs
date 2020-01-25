using System;
using System.Collections.Generic;
using AutoMapper;
using Todo.Domain.Entities;
using Todo.Domain.Enums;
using Todo.Models.Common;
using Todo.Models.Mappings;

namespace Todo.Models.TodoItems
{
    public class TodoItemDetails : IMaps<TodoItem>
    {
        public Guid ItemId { get; set; }
        public Guid? ParentItemId { get; set; }
        public CreatedAuditInfo Created { get; set; }
        public ModifiedAuditInfo Modified { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? StartedOn { get; set; }
        public DateTime? CancelledOn { get; set; }
        public DateTime? CompletedOn { get; set; }
        public TodoItemStatus Status { get; set; }
        public PriorityLevel Priority { get; set; }
        public ImportanceLevel Importance { get; set; }
        public ICollection<TodoItemDetails> ChildItems { get; set; }
        //public ICollection<TodoNoteDetails> Notes { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<TodoItem, TodoItemDetails>()
                .ForMember(i => i.Status, o => o.MapFrom(item => item.GetStatus()))
                .ForMember(m => m.Importance, o => o.MapFrom(m => m.ImportanceLevel))
                .ForMember(m => m.Priority, o => o.MapFrom(m => m.PriorityLevel))
                .ForMember(i => i.ChildItems, o => o.MapFrom(item => item.ChildItems))
                .ForMember(i => i.Created, o => o.MapFrom(i => i))
                .ForMember(i => i.Modified, o => o.MapFrom(i => i));
        }
    }
}