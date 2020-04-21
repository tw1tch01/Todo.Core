using System;
using System.Collections.Generic;
using AutoMapper;
using Todo.Domain.Entities;
using Todo.Domain.Enums;
using Todo.DomainModels.Common;
using Todo.DomainModels.Mappings;
using Todo.DomainModels.TodoNotes;

namespace Todo.DomainModels.TodoItems
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
        public ImportanceLevel Importance { get; set; }
        public PriorityLevel Priority { get; set; }
        public ICollection<TodoItemLookup> ChildItems { get; set; }
        public ICollection<NoteDetails> Notes { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<TodoItem, TodoItemDetails>()
                .ForMember(i => i.Created, o => o.MapFrom(i => i))
                .ForMember(i => i.Modified, o => o.MapFrom(i => i))
                .ForMember(i => i.Status, o => o.MapFrom(i => i.GetStatus()))
                .ForMember(i => i.Importance, o => o.MapFrom(i => i.ImportanceLevel))
                .ForMember(i => i.Priority, o => o.MapFrom(i => i.PriorityLevel))
                .ForMember(i => i.ChildItems, o => o.MapFrom(i => i.ChildItems))
                .ForMember(i => i.Notes, o => o.MapFrom(i => i.Notes));
        }
    }
}