using System;
using System.Collections.Generic;
using FluentValidation.Results;

namespace Todo.Services.TodoNotes.Validation
{
    internal static class NoteValidationResultFactory
    {
        #region Invalid Results

        public static NoteValidationResult InvalidDto(IList<ValidationFailure> failures)
        {
            return new InvalidDtoResult(failures);
        }

        public static NoteValidationResult ItemNotFound(Guid itemid)
        {
            return new ItemNotFoundResult(itemid);
        }

        public static NoteValidationResult NoteNotFound(Guid noteId)
        {
            return new NoteNotFoundResult(noteId);
        }

        public static NoteValidationResult ItemIdMismatch(Guid parentNoteItemId, Guid replyItemId)
        {
            return new ItemIdMismatchResult(parentNoteItemId, replyItemId);
        }

        #endregion Invalid Results

        #region Valid Results

        public static NoteValidationResult NoteCreated(Guid noteId, DateTime createdOn)
        {
            return new NoteCreatedResult(noteId, createdOn);
        }

        public static NoteValidationResult NoteDeleted(Guid noteId, DateTime deletedOn)
        {
            return new NoteDeletedResult(noteId, deletedOn);
        }

        public static NoteValidationResult NoteUpdated(Guid noteId, DateTime modifiedOn)
        {
            return new NoteUpdatedResult(noteId, modifiedOn);
        }

        #endregion Valid Results
    }
}