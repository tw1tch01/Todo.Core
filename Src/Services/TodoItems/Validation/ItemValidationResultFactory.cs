using System;
using System.Collections.Generic;
using FluentValidation.Results;

namespace Todo.Services.TodoItems.Validation
{
    public static class ItemValidationResultFactory
    {
        #region Invalid Results

        public static ItemValidationResult InvalidDto(IList<ValidationFailure> failures)
        {
            return new InvalidDtoResult(failures);
        }

        public static ItemValidationResult ItemAlreadyStarted(Guid itemId, DateTime startedOn)
        {
            return new ItemAlreadyStartedResult(itemId, startedOn);
        }

        public static ItemValidationResult ItemPreviouslyCancelled(Guid itemId, DateTime cancelledOn)
        {
            return new ItemPreviouslyCancelledResult(itemId, cancelledOn);
        }

        public static ItemValidationResult ItemPreviouslyCompleted(Guid itemId, DateTime completedOn)
        {
            return new ItemPreviouslyCompletedResult(itemId, completedOn);
        }

        public static ItemValidationResult ItemNotFound(Guid itemId)
        {
            return new ItemNotFoundResult(itemId);
        }

        #endregion Invalid Results

        #region Valid Results

        public static ItemValidationResult ItemCancelled(Guid itemId, DateTime cancelledOn)
        {
            return new ItemCancelledResult(itemId, cancelledOn);
        }

        public static ItemValidationResult ItemCompleted(Guid itemId, DateTime completedOn)
        {
            return new ItemCompletedResult(itemId, completedOn);
        }

        public static ItemValidationResult ItemCreated(Guid itemId, DateTime createdOn)
        {
            return new ItemCreatedResult(itemId, createdOn);
        }

        public static ItemValidationResult ItemDeleted(Guid itemId, DateTime deletedOn)
        {
            return new ItemDeletedResult(itemId, deletedOn);
        }

        public static ItemValidationResult ItemReset(Guid itemId, DateTime resetOn)
        {
            return new ItemResetResult(itemId, resetOn);
        }

        public static ItemValidationResult ItemStarted(Guid itemId, DateTime startedOn)
        {
            return new ItemStartedResult(itemId, startedOn);
        }

        public static ItemValidationResult ItemUpdated(Guid itemId, DateTime modifiedOn)
        {
            return new ItemUpdatedResult(itemId, modifiedOn);
        }

        #endregion Valid Results
    }
}