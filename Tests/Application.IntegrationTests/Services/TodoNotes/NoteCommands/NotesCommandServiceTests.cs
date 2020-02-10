using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Todo.Application.IntegrationTests.TestingFactory;
using Todo.Application.Services.TodoNotes.NoteCommands;
using Todo.Factories;
using Todo.Services.TodoNotes.Commands.CreateNote;
using Todo.Services.TodoNotes.Commands.DeleteNote;
using Todo.Services.TodoNotes.Commands.UpdateNote;

namespace Todo.Application.IntegrationTests.Services.TodoNotes.NoteCommands
{
    [TestFixture]
    public class NotesCommandServiceTests : MemorySetupFixture
    {
        private NotesCommandService _commandService;

        [OneTimeSetUp]
        public void Init()
        {
            _commandService = new NotesCommandService
            (
                _provider.GetRequiredService<ICreateNoteService>(),
                _provider.GetRequiredService<IDeleteNoteService>(),
                _provider.GetRequiredService<IUpdateNoteService>()
            );
        }

        [Test]
        public async Task CreateNote_IntegrationTest()
        {
            var item = TodoItemFactory.GenerateItem();
            _memoryContext.Add(item);
            _memoryContext.SaveChanges();

            var noteDto = TodoNoteFactory.GenerateCreateNoteDto();

            var _ = await _commandService.CreateNote(item.ItemId, noteDto);

            Assert.Pass();
        }

        [Test]
        public async Task DeleteNote_IntegrationTest()
        {
            var item = TodoItemFactory.GenerateItem();
            var note = TodoNoteFactory.GenerateNote(item.ItemId);
            _memoryContext.Add(item);
            _memoryContext.Add(note);
            _memoryContext.SaveChanges();

            await _commandService.DeleteNote(note.NoteId);

            Assert.Pass();
        }

        [Test]
        public async Task ReplyOnNote_IntegrationTest()
        {
            var item = TodoItemFactory.GenerateItem();
            var note = TodoNoteFactory.GenerateNote(item.ItemId);
            _memoryContext.Add(item);
            _memoryContext.Add(note);
            _memoryContext.SaveChanges();

            var noteDto = TodoNoteFactory.GenerateCreateNoteDto();

            var _ = await _commandService.ReplyOnNote(note.NoteId, noteDto);

            Assert.Pass();
        }

        [Test]
        public async Task UpdateNote_IntegrationTest()
        {
            var item = TodoItemFactory.GenerateItem();
            var note = TodoNoteFactory.GenerateNote(item.ItemId);
            _memoryContext.Add(item);
            _memoryContext.Add(note);
            _memoryContext.SaveChanges();

            var noteDto = TodoNoteFactory.GenerateUpdateNoteDto();

            await _commandService.UpdateNote(note.NoteId, noteDto);

            Assert.Pass();
        }
    }
}