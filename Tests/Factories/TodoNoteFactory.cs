using System;
using AutoFixture;
using Todo.Domain.Entities;
using Todo.DomainModels.TodoNotes;

namespace Todo.Factories
{
    public class TodoNoteFactory
    {
        private static readonly Fixture _fixture = new Fixture();

        public static TodoItemNote GenerateNote(Guid itemId, Guid? parentNoteId = null)
        {
            return new TodoItemNote
            {
                CreatedBy = _fixture.Create<string>(),
                CreatedOn = _fixture.Create<DateTime>(),
                CreatedProcess = _fixture.Create<string>(),
                ModifiedBy = _fixture.Create<string>(),
                ModifiedOn = _fixture.Create<DateTime?>(),
                ModifiedProcess = _fixture.Create<string>(),
                NoteId = Guid.NewGuid(),
                ItemId = itemId,
                ParentNoteId = parentNoteId,
                Comment = _fixture.Create<string>()
            };
        }

        public static CreateNoteDto GenerateCreateNoteDto()
        {
            return new CreateNoteDto
            {
                Comment = _fixture.Create<string>()
            };
        }

        public static UpdateNoteDto GenerateUpdateNoteDto()
        {
            return new UpdateNoteDto
            {
                Comment = _fixture.Create<string>()
            };
        }

        public static NoteDetails GenerateNoteDetails()
        {
            return new NoteDetails
            {
            };
        }
    }
}