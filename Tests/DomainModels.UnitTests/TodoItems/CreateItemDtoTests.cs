using NUnit.Framework;
using Todo.Domain.Entities;
using Todo.DomainModels.TodoItems;
using Todo.DomainModels.UnitTests.Mappings;
using Todo.Factories;

namespace Todo.DomainModels.UnitTests.TodoItems
{
    [TestFixture]
    public class CreateItemDtoTests : MappingSetUpFixture
    {
        [Test]
        public void NullObjectReturnsNull()
        {
            CreateItemDto dto = null;
            var item = _mapper.Map<TodoItem>(dto);
            Assert.IsNull(item);
        }

        [Test]
        public void MapsCreateItemDtoToTodoItem()
        {
            var dto = TodoItemFactory.GenerateCreateDto();
            var item = _mapper.Map<TodoItem>(dto);

            Assert.Multiple(() =>
            {
                Assert.IsNotNull(item);
                Assert.IsInstanceOf<TodoItem>(item);
                Assert.AreEqual(dto.Name, item.Name);
                Assert.AreEqual(dto.Description, item.Description);
                Assert.AreEqual(dto.Rank, item.Rank);
                Assert.AreEqual(dto.DueDate, item.DueDate);
                Assert.AreEqual(dto.Importance, item.ImportanceLevel);
                Assert.AreEqual(dto.Priority, item.PriorityLevel);
                Assert.IsNull(item.ParentItemId);
                Assert.IsNull(item.StartedOn);
                Assert.IsNull(item.CancelledOn);
                Assert.IsNull(item.CompletedOn);
                Assert.IsEmpty(item.ChildItems);
                Assert.IsEmpty(item.Notes);
            });
        }
    }
}