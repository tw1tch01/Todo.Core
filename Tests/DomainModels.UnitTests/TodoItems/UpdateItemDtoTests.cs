using System;
using AutoFixture;
using NUnit.Framework;
using Todo.Domain.Enums;
using Todo.DomainModels.TodoItems;
using Todo.DomainModels.UnitTests.Mappings;
using Todo.Factories;

namespace Todo.DomainModels.UnitTests.TodoItems
{
    [TestFixture]
    public class UpdateItemDtoTests : MappingSetUpFixture
    {
        private readonly Fixture _fixture = new Fixture();

        [Test]
        public void Map_WhenAllPropertiesAreNull_LeavesItemUnchanged()
        {
            var dto = new UpdateItemDto
            {
                Name = null,
                Description = null,
                DueDate = null,
                Importance = null,
                Priority = null,
                Rank = null
            };
            var item = TodoItemFactory.GenerateItem();
            _mapper.Map(dto, item);

            Assert.Multiple(() =>
            {
                Assert.IsNotNull(item.Name);
                Assert.IsNotNull(item.Description);
                Assert.IsNotNull(item.DueDate);
                Assert.IsNotNull(item.ImportanceLevel);
                Assert.IsNotNull(item.PriorityLevel);
                Assert.IsNotNull(item.Rank);
            });
        }

        [TestCase("")]
        [TestCase(" ")]
        public void Map_WhenNameIsEmptyString_LeavesItemUnchanged(string name)
        {
            var dto = new UpdateItemDto
            {
                Name = name
            };
            var item = TodoItemFactory.GenerateItem();
            _mapper.Map(dto, item);

            Assert.AreNotEqual(dto.Name, item.Name);
        }

        [Test]
        public void Map_WhenNameIsNotNull_SetsName()
        {
            var dto = new UpdateItemDto
            {
                Name = _fixture.Create<string>()
            };
            var item = TodoItemFactory.GenerateItem();
            _mapper.Map(dto, item);

            Assert.AreEqual(dto.Name, item.Name);
        }

        [Test]
        public void Map_WhenDescriptionIsNotNull_SetsDescription()
        {
            var dto = new UpdateItemDto
            {
                Description = _fixture.Create<string>()
            };
            var item = TodoItemFactory.GenerateItem();
            _mapper.Map(dto, item);

            Assert.AreEqual(dto.Description, item.Description);
        }

        [Test]
        public void Map_WhenRankIsNotNull_SetsRank()
        {
            var dto = new UpdateItemDto
            {
                Rank = _fixture.Create<int>()
            };
            var item = TodoItemFactory.GenerateItem();
            _mapper.Map(dto, item);

            Assert.AreEqual(dto.Rank, item.Rank);
        }

        [Test]
        public void Map_WhenDueDateIsNotNull_SetsDueDate()
        {
            var dto = new UpdateItemDto
            {
                DueDate = _fixture.Create<DateTime>()
            };
            var item = TodoItemFactory.GenerateItem();
            _mapper.Map(dto, item);

            Assert.AreEqual(dto.DueDate, item.DueDate);
        }

        [Test]
        public void Map_WhenImportanceIsNotNull_SetsImportanceLevel()
        {
            var dto = new UpdateItemDto
            {
                Importance = _fixture.Create<ImportanceLevel>()
            };
            var item = TodoItemFactory.GenerateItem();
            _mapper.Map(dto, item);

            Assert.AreEqual(dto.Importance, item.ImportanceLevel);
        }

        [Test]
        public void Map_WhenPriorityIsNotNull_SetsPriorityLevel()
        {
            var dto = new UpdateItemDto
            {
                Priority = _fixture.Create<PriorityLevel>()
            };
            var item = TodoItemFactory.GenerateItem();
            _mapper.Map(dto, item);

            Assert.AreEqual(dto.Priority, item.PriorityLevel);
        }
    }
}