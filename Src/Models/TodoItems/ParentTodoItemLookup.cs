using AutoMapper;
using Todo.Domain.Entities;
using Todo.Models.Mappings;

namespace Todo.Models.TodoItems
{
    public class ParentTodoItemLookup : TodoItemLookup, IMaps<TodoItem>
    {
        public int ChildItems { get; set; }
        public int Notes { get; set; }

        public override void Mapping(Profile profile)
        {
            profile.CreateMap<TodoItem, ParentTodoItemLookup>()
                .ForMember(m => m.Importance, o => o.MapFrom(m => m.ImportanceLevel))
                .ForMember(m => m.Priority, o => o.MapFrom(m => m.PriorityLevel))
                .ForMember(m => m.ChildItems, o => o.MapFrom(m => m.ChildItems.Count))
                .ForMember(m => m.Notes, o => o.MapFrom(m => m.Notes.Count));
        }
    }
}