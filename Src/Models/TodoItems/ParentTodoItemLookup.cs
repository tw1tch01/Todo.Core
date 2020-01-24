using System;
using AutoMapper;
using Todo.Domain.Entities;
using Todo.Domain.Enums;
using Todo.Models.Mappings;

namespace Todo.Models.TodoItems
{
    public class ParentTodoItemLookup : IMaps<TodoItem>
    {
        public Guid ItemId { get; set; }
        public string Name { get; set; }
        public DateTime? DueDate { get; set; }
        public ImportanceLevel Importance { get; set; }
        public PriorityLevel Priority { get; set; }
        public TodoItemStatus Status { get; set; }
        public int ChildItems { get; set; }
        public int Notes { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<TodoItem, ParentTodoItemLookup>()
                .ForMember(m => m.ChildItems, o => o.MapFrom(m => m.ChildItems.Count))
                .ForMember(m => m.Status, o => o.MapFrom(m => m.GetStatus()))
                .ForMember(m => m.Notes, o => o.MapFrom(m => m.Notes.Count));
            //.ForMember(m => m.ItemId, o => o.MapFrom(m => m.ItemId))
        }
    }
}