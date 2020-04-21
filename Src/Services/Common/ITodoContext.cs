using Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Todo.Domain.Entities;

namespace Todo.Services.Common
{
    public interface ITodoContext : IAuditedContext
    {
        DbSet<TodoItem> Items { get; }
        DbSet<TodoItemNote> Notes { get; }
    }
}