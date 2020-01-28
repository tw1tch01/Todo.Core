using System.Linq;
using NUnit.Framework;
using Todo.Domain.Entities;
using Todo.Factories;
using Todo.DomainModels.TodoItems;
using Todo.DomainModels.UnitTests.Mappings;

namespace Todo.DomainModels.UnitTests.TodoItems
{
    [TestFixture]
    public class TodoItemDetailsTests : MappingSetUpFixture
    {
        [Test]
        public void NullObjectReturnsNull()
        {
            TodoItem item = null;
            var details = _mapper.Map<TodoItemDetails>(item);
            Assert.IsNull(details);
        }

        [Test]
        public void MapsItemToItemDetails()
        {
            var item = TodoItemFactory.GenerateItem();
            var details = _mapper.Map<TodoItemDetails>(item);

            Assert.Multiple(() =>
            {
                Assert.IsNotNull(details);
                Assert.IsInstanceOf<TodoItemDetails>(details);
                Assert.AreEqual(item.CreatedBy, details.Created.By);
                Assert.AreEqual(item.CreatedOn, details.Created.On);
                Assert.AreEqual(item.CreatedProcess, details.Created.Process);
                Assert.AreEqual(item.ModifiedBy, details.Modified.By);
                Assert.AreEqual(item.ModifiedOn, details.Modified.On);
                Assert.AreEqual(item.ModifiedProcess, details.Modified.Process);
                Assert.AreEqual(item.ItemId, details.ItemId);
                Assert.AreEqual(item.ParentItemId, details.ParentItemId);
                Assert.AreEqual(item.Name, details.Name);
                Assert.AreEqual(item.Description, details.Description);
                Assert.AreEqual(item.DueDate, details.DueDate);
                Assert.AreEqual(item.StartedOn, details.StartedOn);
                Assert.AreEqual(item.CancelledOn, details.CancelledOn);
                Assert.AreEqual(item.CompletedOn, details.CompletedOn);
                Assert.AreEqual(item.GetStatus(), details.Status);
            });
        }

        [Test]
        public void MapsItemWithChildItemsToItemDetails()
        {
            var parentItem = TodoItemFactory.GenerateItemWithChildren(1);
            var childItem = parentItem.ChildItems.First();

            var parentDetails = _mapper.Map<TodoItemDetails>(parentItem);
            var childDetails = parentDetails.ChildItems.First();

            Assert.Multiple(() =>
            {
                Assert.IsNotNull(parentDetails);
                Assert.IsInstanceOf<TodoItemDetails>(parentDetails);
                Assert.AreEqual(parentItem.CreatedBy, parentDetails.Created.By);
                Assert.AreEqual(parentItem.CreatedOn, parentDetails.Created.On);
                Assert.AreEqual(parentItem.CreatedProcess, parentDetails.Created.Process);
                Assert.AreEqual(parentItem.ModifiedBy, parentDetails.Modified.By);
                Assert.AreEqual(parentItem.ModifiedOn, parentDetails.Modified.On);
                Assert.AreEqual(parentItem.ModifiedProcess, parentDetails.Modified.Process);
                Assert.AreEqual(parentItem.ItemId, parentDetails.ItemId);
                Assert.AreEqual(parentItem.ParentItemId, parentDetails.ParentItemId);
                Assert.AreEqual(parentItem.Name, parentDetails.Name);
                Assert.AreEqual(parentItem.Description, parentDetails.Description);
                Assert.AreEqual(parentItem.DueDate, parentDetails.DueDate);
                Assert.AreEqual(parentItem.StartedOn, parentDetails.StartedOn);
                Assert.AreEqual(parentItem.CancelledOn, parentDetails.CancelledOn);
                Assert.AreEqual(parentItem.CompletedOn, parentDetails.CompletedOn);
                Assert.AreEqual(parentItem.GetStatus(), parentDetails.Status);
                Assert.AreEqual(parentItem.ImportanceLevel, parentDetails.Importance);
                Assert.AreEqual(parentItem.PriorityLevel, parentDetails.Priority);
                Assert.IsNotNull(childDetails);
                Assert.IsInstanceOf<TodoItemLookup>(childDetails);
                Assert.AreEqual(childItem.ItemId, childDetails.ItemId);
                Assert.AreEqual(childItem.Name, childDetails.Name);
                Assert.AreEqual(childItem.DueDate, childDetails.DueDate);
                Assert.AreEqual(childItem.GetStatus(), childDetails.Status);
            });
        }
    }
}