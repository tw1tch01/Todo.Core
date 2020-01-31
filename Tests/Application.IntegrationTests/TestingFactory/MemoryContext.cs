using System;
using System.Collections.Generic;
using AutoFixture;
using Data.Common;
using Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Todo.Domain.Common;
using Todo.Domain.Entities;
using Todo.Factories;
using Todo.Services.Common;

namespace Todo.Application.IntegrationTests.TestingFactory
{
    public class MemoryContext : DbContext, ITodoContext
    {
        internal const string CreatedBy = "Integration-Tests";
        internal const string CreatedProcess = "/Integration-Tests";
        internal const string ModifiedBy = "Integration-Tests";
        internal const string ModifiedProcess = "/Integration-Tests";

        private readonly Fixture _fixture = new Fixture();

        public MemoryContext(DbContextOptions<MemoryContext> options) : base(options)
        {
            ContextScope = new ContextScope();
            ContextScope.StateActions.Add(EntityState.Added, SetCreatedAuditFields);
            ContextScope.StateActions.Add(EntityState.Modified, SetModifiedAuditFields);
        }

        public ContextScope ContextScope { get; }

        public DbSet<TodoItem> Items { get; }

        public DbSet<TodoItemNote> Notes { get; }

        public void Seed()
        {
            var entities = new List<TodoItem>();

            for (int i = 0; i < 25; i++) entities.Add(TodoItemFactory.GenerateItem());
            for (int i = 0; i < 25; i++) entities.Add(TodoItemFactory.GenerateItemWithChildren(_fixture.Create<int>() % 5));

            AddRange(entities);
            SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new TodoItemConfiguration());
            modelBuilder.ApplyConfiguration(new TodoNoteConfiguration());
        }

        private void SetCreatedAuditFields(EntityEntry entity)
        {
            entity.Entity.TrySetProperty(nameof(ICreatedAudit.CreatedBy), CreatedBy)
                         .TrySetProperty(nameof(ICreatedAudit.CreatedOn), DateTime.UtcNow)
                         .TrySetProperty(nameof(ICreatedAudit.CreatedProcess), CreatedProcess);
        }

        private void SetModifiedAuditFields(EntityEntry entity)
        {
            entity.Entity.TrySetProperty(nameof(IModifiedAudit.ModifiedBy), ModifiedBy)
                         .TrySetProperty(nameof(IModifiedAudit.ModifiedOn), DateTime.UtcNow)
                         .TrySetProperty(nameof(IModifiedAudit.ModifiedProcess), ModifiedProcess);
        }
    }
}