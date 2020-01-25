using System;
using AutoMapper;
using Todo.Domain.Entities;
using Todo.Domain.Enums;
using Todo.Models.Mappings;

namespace Todo.Models.TodoItems
{
    public class UpdateItemDto : IMaps<TodoItem>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int? Rank { get; set; }
        public DateTime? DueDate { get; set; }
        public ImportanceLevel? Importance { get; set; }
        public PriorityLevel? Priority { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateItemDto, TodoItem>()
                .ForMember(m => m.Name, o => o.Condition(c => !string.IsNullOrWhiteSpace(c.Name)))
                .ForMember(m => m.Description, o => o.Condition(c => !string.IsNullOrWhiteSpace(c.Description)))
                .ForMember(m => m.Rank, o => o.Condition(c => c.Rank >= 0))
                .ForMember(m => m.DueDate, o => o.Condition(c => c.DueDate.HasValue))
                .ForMember(m => m.ImportanceLevel, o =>
                {
                    o.Condition(c => c.Importance.HasValue);
                    o.MapFrom(m => m.Importance);
                })
                .ForMember(m => m.PriorityLevel, o =>
                {
                    o.Condition(c => c.Priority.HasValue);
                    o.MapFrom(m => m.Priority);
                });
        }
    }
}