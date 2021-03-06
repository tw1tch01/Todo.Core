﻿using System;
using System.Linq.Expressions;
using Data.Specifications;
using Todo.Domain.Entities;

namespace Todo.Services.TodoNotes.Specifications
{
    public class GetNoteById : LinqSpecification<TodoItemNote>
    {
        public GetNoteById(Guid noteId)
        {
            NoteId = noteId;
        }

        public Guid NoteId { get; }

        public override Expression<Func<TodoItemNote, bool>> AsExpression()
        {
            return note => note.NoteId == NoteId;
        }
    }
}