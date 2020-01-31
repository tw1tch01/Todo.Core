using System;
using AutoMapper;
using Data.Repositories;
using MediatR;
using Moq;
using NUnit.Framework;
using Todo.DomainModels.TodoNotes;
using Todo.Services.Common;
using Todo.Services.Common.Exceptions;
using Todo.Services.TodoNotes.Commands.UpdateNote;
using Todo.Services.TodoNotes.Specifications;

namespace Todo.Services.UnitTests.TodoNotes.Commands.UpdateNote
{
    [TestFixture]
    public class UpdateNoteServiceTests
    {
        [Test]
        public void UpdateNote_WhenUpdateNoteDtoIsNull_ThrowsArgumentNullException()
        {
            UpdateNoteDto noteDto = null;
            var mockRepository = new Mock<IContextRepository<ITodoContext>>();
            var mockMapper = new Mock<IMapper>();
            var mockMediator = new Mock<IMediator>();

            var service = new UpdateNoteService(mockRepository.Object, mockMapper.Object, mockMediator.Object);

            Assert.ThrowsAsync<ArgumentNullException>(() => service.UpdateNote(Guid.NewGuid(), noteDto));
        }

        [Test]
        public void UpdateNote_WhenNoteDoesNotExist_ThrowsNotFoundException()
        {
            var noteId = Guid.NewGuid();
            var noteDto = new UpdateNoteDto();
            var mockRepository = new Mock<IContextRepository<ITodoContext>>();
            var mockMapper = new Mock<IMapper>();
            var mockMediator = new Mock<IMediator>();

            mockRepository.Setup(m => m.GetAsync(It.Is<GetNoteById>(a => a.NoteId == noteId))).ReturnsAsync(() => null);

            var service = new UpdateNoteService(mockRepository.Object, mockMapper.Object, mockMediator.Object);

            Assert.ThrowsAsync<NotFoundException>(() => service.UpdateNote(noteId, noteDto));
        }
    }
}