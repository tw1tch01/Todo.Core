using System;
using NUnit.Framework;
using Todo.Domain.Entities;
using Todo.Services.TodoNotes.Specifications;

namespace Todo.Services.UnitTests.TodoNotes.Specifications
{
    [TestFixture]
    public class GetNoteByIdTests
    {
        [Test]
        public void IsSatisfiedBy_WhenItemIdMatchesSpecId_ReturnsTrue()
        {
            var id = Guid.NewGuid();
            var item = new TodoItemNote { NoteId = id };
            var specification = new GetNoteById(id);
            var satisfied = specification.IsSatisfiedBy(item);
            Assert.IsTrue(satisfied);
        }

        [Test]
        public void IsSatisfiedBy_WhenItemIdDoesNotMatchSpecId_ReturnsFalse()
        {
            var note = new TodoItemNote { NoteId = Guid.NewGuid() };
            var specification = new GetNoteById(Guid.NewGuid());
            var satisfied = specification.IsSatisfiedBy(note);
            Assert.IsFalse(satisfied);
        }
    }
}