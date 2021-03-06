﻿using System;
using NUnit.Framework;
using Todo.Domain.Entities;
using Todo.Services.TodoItems.Specifications;

namespace Todo.Services.UnitTests.TodoItems.Specifications
{
    [TestFixture]
    public class GetParentItemTests
    {
        [Test]
        public void IsSatisfiedBy_WhenItemIsNotParentItem_ReturnsFalse()
        {
            var item = new TodoItem { ParentItemId = Guid.NewGuid() };
            var isParentItem = new GetParentItems();
            var satisfied = isParentItem.IsSatisfiedBy(item);
            Assert.IsFalse(satisfied);
        }

        [Test]
        public void IsSatisfiedBy_WhenItemIsParentItem_ReturnsTrue()
        {
            var item = new TodoItem { ParentItemId = null };
            var isParentItem = new GetParentItems();
            var satisfied = isParentItem.IsSatisfiedBy(item);
            Assert.IsTrue(satisfied);
        }
    }
}