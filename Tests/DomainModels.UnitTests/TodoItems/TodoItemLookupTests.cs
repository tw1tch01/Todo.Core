using NUnit.Framework;
using Todo.Domain.Entities;
using Todo.Factories;
using Todo.DomainModels.TodoItems;
using Todo.DomainModels.UnitTests.Mappings;

namespace Todo.DomainModels.UnitTests.TodoItems
{
    [TestFixture]
    public class TodoItemLookupTests : MappingSetUpFixture
    {
        [Test]
        public void NullObjectReturnsNull()
        {
            TodoItem item = null;
            var details = _mapper.Map<TodoItemLookup>(item);
            Assert.IsNull(details);
        }

        [Test]
        public void MapsItemToTodoItemLookup()
        {
            var item = TodoItemFactory.GenerateItem();
            var details = _mapper.Map<TodoItemLookup>(item);

            Assert.Multiple(() =>
            {
                Assert.IsNotNull(details);
                Assert.IsInstanceOf<TodoItemLookup>(details);
                Assert.AreEqual(item.ItemId, details.ItemId);
                Assert.AreEqual(item.Name, details.Name);
                Assert.AreEqual(item.DueDate, details.DueDate);
                Assert.AreEqual(item.ImportanceLevel, details.Importance);
                Assert.AreEqual(item.PriorityLevel, details.Priority);
                Assert.AreEqual(item.GetStatus(), details.Status);
            });
        }
    }
}