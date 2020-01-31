using System;
using NUnit.Framework;
using Todo.Domain.Entities;
using Todo.Services.TodoNotes.Specifications;

namespace Todo.Services.UnitTests.TodoNotes.Specifications
{
    [TestFixture]
    public class GetNotesByItemIdTests
    {
        [Test]
        public void IsSatisfiedBy_WhenParentItemIdDoesNotMatchValuePassedIn_ReturnsFalse()
        {
            var note = new TodoItemNote
            {
                ItemId = Guid.NewGuid()
            };
            var specification = new GetNotesByItemId(Guid.NewGuid());
            var satisfied = specification.IsSatisfiedBy(note);
            Assert.IsFalse(satisfied);
        }

        [Test]
        public void IsSatisfiedBy_WhenParentItemIdMatchesValuePassedIn_ReturnsTrue()
        {
            var itemId = Guid.NewGuid();
            var note = new TodoItemNote
            {
                ItemId = itemId
            };
            var specification = new GetNotesByItemId(itemId);
            var satisfied = specification.IsSatisfiedBy(note);
            Assert.IsTrue(satisfied);
        }
    }
}