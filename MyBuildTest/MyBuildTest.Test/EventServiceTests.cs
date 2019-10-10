using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MyBuildTest.Data.Model;
using MyBuildTest.Data.Provider;
using MyBuildTest.Data.Repositories;
using MyBuildTest.Domain.Service;

namespace MyBuildTest.Test
{
    [TestClass]
    public class EventServiceTests
    {
        [TestMethod]
        public async Task GetEventsWithDuration_ReturnEvents()
        {
            var repositoryMoq = new Mock<IEventRepository>();
            var providerMoq = new Mock<IEventsProvider>();
            providerMoq.Setup(x => x.GetEventsAsync()).ReturnsAsync(new List<EventJson>()
            {
                new EventJson { Id = "1", Timestamp = 1 }, new EventJson { Id = "1", Timestamp = 3 },
                new EventJson { Id = "2", Timestamp = 1 }, new EventJson { Id = "2", Timestamp = 13 }
            });

            var service = new EventService(providerMoq.Object, repositoryMoq.Object);

            var result = await service.GetEventsWithDuration();

            Assert.AreEqual(2, result.Count());
            Assert.AreEqual(2, result.First().Duration);
            Assert.IsFalse(result.First().Alert);

            Assert.AreEqual(12, result.Skip(1).First().Duration);
            Assert.IsFalse(result.Skip(1).First().Alert);
        }
    }
}
