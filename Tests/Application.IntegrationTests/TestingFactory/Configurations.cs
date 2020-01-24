using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Todo.Domain.Entities;

namespace Todo.Application.IntegrationTests.TestingFactory
{
    internal class TodoItemConfiguration : IEntityTypeConfiguration<TodoItem>
    {
        public void Configure(EntityTypeBuilder<TodoItem> builder)
        {
            builder.HasKey(item => item.ItemId);
            builder.HasMany(parentItem => parentItem.ChildItems)
                   .WithOne(childItem => childItem.ParentItem)
                   .HasForeignKey(childItem => childItem.ParentItemId);
        }
    }

    internal class TodoNoteConfiguration : IEntityTypeConfiguration<TodoItemNote>
    {
        public void Configure(EntityTypeBuilder<TodoItemNote> builder)
        {
            builder.HasKey(note => note.NoteId);
            builder.HasOne(note => note.Item)
                   .WithMany(item => item.Notes)
                   .HasForeignKey(note => note.ItemId);
            builder.HasOne(childItem => childItem.ParentNote)
                   .WithMany(parentNote => parentNote.Replies)
                   .HasForeignKey(childNote => childNote.ParentNoteId);
        }
    }
}