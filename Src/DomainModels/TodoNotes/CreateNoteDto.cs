using AutoMapper;
using Todo.Domain.Entities;
using Todo.DomainModels.Mappings;

namespace Todo.DomainModels.TodoNotes
{
    public class CreateNoteDto : IMaps<TodoItemNote>
    {
        public string Comment { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateNoteDto, TodoItemNote>();
        }
    }
}