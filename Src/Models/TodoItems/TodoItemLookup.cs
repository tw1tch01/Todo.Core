using System;
using AutoMapper;
using Todo.Domain.Entities;
using Todo.Domain.Enums;
using Todo.Models.Mappings;

namespace Todo.Models.TodoItems
{
    public class TodoItemLookup : IMaps<TodoItem>
    {
        public Guid ItemId { get; set; }
        public string Name { get; set; }
        public DateTime? DueDate { get; set; }
        public ImportanceLevel Importance { get; set; }
        public PriorityLevel Priority { get; set; }
        public TodoItemStatus Status { get; set; }

        public virtual void Mapping(Profile profile)
        {
            profile.CreateMap<TodoItem, TodoItemLookup>()
                .ForMember(m => m.Status, o => o.MapFrom(m => m.GetStatus()));
        }
    }
}