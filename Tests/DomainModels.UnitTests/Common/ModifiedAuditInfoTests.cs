using System;
using AutoFixture;
using Moq;
using NUnit.Framework;
using Todo.Domain.Common;
using Todo.DomainModels.Common;
using Todo.DomainModels.UnitTests.Mappings;

namespace Todo.DomainModels.UnitTests.Common
{
    [TestFixture]
    public class ModifiedAuditInfoTests : MappingSetUpFixture
    {
        private readonly Fixture _fixture = new Fixture();

        [Test]
        public void NullObjectReturnsNull()
        {
            IModifiedAudit modifiedAudit = null;
            var auditInfo = _mapper.Map<ModifiedAuditInfo>(modifiedAudit);
            Assert.IsNull(auditInfo);
        }

        [Test]
        public void MapsItemToParentTodoItemLookup()
        {
            var mockModifiedAudit = new Mock<IModifiedAudit>();
            mockModifiedAudit.SetupGet(a => a.ModifiedBy).Returns(_fixture.Create<string>());
            mockModifiedAudit.SetupGet(a => a.ModifiedOn).Returns(_fixture.Create<DateTime>());
            mockModifiedAudit.SetupGet(a => a.ModifiedProcess).Returns(_fixture.Create<string>());
            var auditInfo = _mapper.Map<ModifiedAuditInfo>(mockModifiedAudit.Object);

            Assert.Multiple(() =>
            {
                Assert.IsNotNull(auditInfo);
                Assert.IsInstanceOf<ModifiedAuditInfo>(auditInfo);
                Assert.AreEqual(mockModifiedAudit.Object.ModifiedBy, auditInfo.By);
                Assert.AreEqual(mockModifiedAudit.Object.ModifiedOn, auditInfo.On);
                Assert.AreEqual(mockModifiedAudit.Object.ModifiedProcess, auditInfo.Process);
            });
        }
    }
}