using System;
using AutoMapper;
using Todo.Domain.Entities;
using Todo.Domain.Enums;
using Todo.Models.Mappings;

namespace Todo.Models.TodoItems
{
    public class CreateItemDto : IMaps<TodoItem>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Rank { get; set; }
        public DateTime? DueDate { get; set; }
        public ImportanceLevel Importance { get; set; }
        public PriorityLevel Priority { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateItemDto, TodoItem>()
                .ForMember(m => m.ImportanceLevel, o => o.MapFrom(m => m.Importance))
                .ForMember(m => m.PriorityLevel, o => o.MapFrom(m => m.Priority));
        }
    }
}