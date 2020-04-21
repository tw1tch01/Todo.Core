using System;
using System.Linq;
using NUnit.Framework;
using Todo.DomainModels.TodoNotes;
using Todo.DomainModels.UnitTests.Mappings;
using Todo.Factories;

namespace Todo.DomainModels.UnitTests.TodoNotes
{
    [TestFixture]
    public class NoteDetailsTests : MappingSetUpFixture
    {
        [Test]
        public void NullObjectReturnsNull()
        {
            NoteDetails note = null;
            var details = _mapper.Map<NoteDetails>(note);
            Assert.IsNull(details);
        }

        [Test]
        public void MapsNoteToNoteDetails()
        {
            var note = TodoNoteFactory.GenerateNote(Guid.NewGuid());
            var details = _mapper.Map<NoteDetails>(note);

            Assert.Multiple(() =>
            {
                Assert.IsNotNull(details);
                Assert.IsInstanceOf<NoteDetails>(details);
                Assert.AreEqual(note.CreatedBy, details.Created.By);
                Assert.AreEqual(note.CreatedOn, details.Created.On);
                Assert.AreEqual(note.CreatedProcess, details.Created.Process);
                Assert.AreEqual(note.ModifiedBy, details.Modified.By);
                Assert.AreEqual(note.ModifiedOn, details.Modified.On);
                Assert.AreEqual(note.ModifiedProcess, details.Modified.Process);
                Assert.AreEqual(note.NoteId, details.NoteId);
                Assert.AreEqual(note.Comment, details.Comment);
            });
        }

        [Test]
        public void MapsNoteWithChildNotesToNoteDetails()
        {
            var note = TodoNoteFactory.GenerateNote(Guid.NewGuid());
            var reply = TodoNoteFactory.GenerateNote(note.ItemId, note.NoteId);
            note.Replies.Add(reply);

            var noteDetails = _mapper.Map<NoteDetails>(note);
            var replyDetails = noteDetails.Replies.First();

            Assert.Multiple(() =>
            {
                Assert.IsNotNull(noteDetails);
                Assert.IsInstanceOf<NoteDetails>(noteDetails);
                Assert.AreEqual(note.CreatedBy, noteDetails.Created.By);
                Assert.AreEqual(note.CreatedOn, noteDetails.Created.On);
                Assert.AreEqual(note.CreatedProcess, noteDetails.Created.Process);
                Assert.AreEqual(note.ModifiedBy, noteDetails.Modified.By);
                Assert.AreEqual(note.ModifiedOn, noteDetails.Modified.On);
                Assert.AreEqual(note.ModifiedProcess, noteDetails.Modified.Process);
                Assert.AreEqual(note.NoteId, noteDetails.NoteId);
                Assert.IsNull(noteDetails.ParentNoteId);
                Assert.AreEqual(note.Comment, noteDetails.Comment);
                Assert.IsNotNull(replyDetails);
                Assert.IsInstanceOf<NoteDetails>(replyDetails);
                Assert.AreEqual(reply.CreatedBy, replyDetails.Created.By);
                Assert.AreEqual(reply.CreatedOn, replyDetails.Created.On);
                Assert.AreEqual(reply.CreatedProcess, replyDetails.Created.Process);
                Assert.AreEqual(reply.ModifiedBy, replyDetails.Modified.By);
                Assert.AreEqual(reply.ModifiedOn, replyDetails.Modified.On);
                Assert.AreEqual(reply.ModifiedProcess, replyDetails.Modified.Process);
                Assert.AreEqual(reply.NoteId, replyDetails.NoteId);
                Assert.AreEqual(reply.ParentNoteId, replyDetails.ParentNoteId);
                Assert.AreEqual(reply.Comment, replyDetails.Comment);
            });
        }
    }
}