using System.Collections.Generic;
using AutoMapper;
using Todo.Domain.Entities;
using Todo.DomainModels.Common;
using Todo.DomainModels.Mappings;

namespace Todo.DomainModels.TodoNotes
{
    public class NoteDetails : IMaps<TodoItemNote>
    {
        public int NoteId { get; set; }
        public CreatedAuditInfo Created { get; set; }
        public ModifiedAuditInfo Modified { get; set; }
        public string Comment { get; set; }

        public ICollection<NoteDetails> Replies { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<TodoItemNote, NoteDetails>()
                .ForMember(m => m.Created, o => o.MapFrom(m => m))
                .ForMember(m => m.Modified, o => o.MapFrom(m => m))
                .ForMember(m => m.Replies, o => o.MapFrom(m => m.Replies));
        }
    }
}