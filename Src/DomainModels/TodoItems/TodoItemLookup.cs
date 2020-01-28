using System;
using AutoMapper;
using Todo.Domain.Entities;
using Todo.Domain.Enums;
using Todo.DomainModels.Mappings;

namespace Todo.DomainModels.TodoItems
{
    public class TodoItemLookup : IMaps<TodoItem>
    {
        public Guid ItemId { get; set; }
        public string Name { get; set; }
        public DateTime? DueDate { get; set; }
        public ImportanceLevel Importance { get; set; }
        public PriorityLevel Priority { get; set; }
        public TodoItemStatus Status { get; set; }
        public int Notes { get; set; }

        public virtual void Mapping(Profile profile)
        {
            profile.CreateMap<TodoItem, TodoItemLookup>()
                .ForMember(m => m.Importance, o => o.MapFrom(m => m.ImportanceLevel))
                .ForMember(m => m.Priority, o => o.MapFrom(m => m.PriorityLevel))
                .ForMember(m => m.Notes, o => o.MapFrom(m => m.Notes.Count))
                .ForMember(m => m.Status, o => o.MapFrom(m => m.GetStatus()));
        }
    }
}