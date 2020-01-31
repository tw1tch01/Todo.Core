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
    public class CreatedAuditInfoTests : MappingSetUpFixture
    {
        private readonly Fixture _fixture = new Fixture();

        [Test]
        public void NullObjectReturnsNull()
        {
            ICreatedAudit createdAudit = null;
            var auditInfo = _mapper.Map<CreatedAuditInfo>(createdAudit);
            Assert.IsNull(auditInfo);
        }

        [Test]
        public void MapsItemToParentTodoItemLookup()
        {
            var mockCreatedAudit = new Mock<ICreatedAudit>();
            mockCreatedAudit.SetupGet(a => a.CreatedBy).Returns(_fixture.Create<string>());
            mockCreatedAudit.SetupGet(a => a.CreatedOn).Returns(_fixture.Create<DateTime>());
            mockCreatedAudit.SetupGet(a => a.CreatedProcess).Returns(_fixture.Create<string>());
            var auditInfo = _mapper.Map<CreatedAuditInfo>(mockCreatedAudit.Object);

            Assert.Multiple(() =>
            {
                Assert.IsNotNull(auditInfo);
                Assert.IsInstanceOf<CreatedAuditInfo>(auditInfo);
                Assert.AreEqual(mockCreatedAudit.Object.CreatedBy, auditInfo.By);
                Assert.AreEqual(mockCreatedAudit.Object.CreatedOn, auditInfo.On);
                Assert.AreEqual(mockCreatedAudit.Object.CreatedProcess, auditInfo.Process);
            });
        }
    }
}