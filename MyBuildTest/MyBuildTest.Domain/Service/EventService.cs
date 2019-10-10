using MyBuildTest.Data.Model;
using MyBuildTest.Data.Provider;
using MyBuildTest.Data.Repositories;
using MyBuildTest.Domain.Common;
using MyBuildTest.Domain.Dtos;
using MyBuildTest.Domain.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBuildTest.Domain.Service
{
    public interface IEventService
    {
        Task SaveAsync();
        Task<IEnumerable<Event>> GetEventsWithDuration();
    }

    public class EventService: IEventService
    {
        private readonly IEventsProvider eventsProvider;
        private readonly IEventRepository eventRepository;

        public EventService(
            IEventsProvider eventsProvider,
            IEventRepository eventRepository
            )
        {
            this.eventsProvider = eventsProvider;
            this.eventRepository = eventRepository;
        }

        public async Task SaveAsync()
        {
            var events = await GetEventsWithDuration();
            await eventRepository.AddEventsAsync(events);
        }

        public async Task<IEnumerable<Event>> GetEventsWithDuration()
        {
            var events = await eventsProvider.GetEventsAsync();
            var result = from e in events group e by e.Id into grp select new Event { Id = grp.Key, Duration = grp.Select(x => x.Timestamp).GetTotalDuration(), Host = grp.Select(x => x.Host).FirstOrDefault(), Type = grp.Select(x => x.Type).FirstOrDefault() };
            result.ToList().ForEach(x => x.Alert = x.Duration >= AlertFlag.Duration ? true : false);

            return result;
        }
    }
}
