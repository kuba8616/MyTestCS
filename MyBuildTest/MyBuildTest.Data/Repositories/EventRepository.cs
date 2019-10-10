using LiteDB;
using MyBuildTest.Data.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyBuildTest.Data.Repositories
{
    public interface IEventRepository
    {
        Task AddEventsAsync(IEnumerable<Event> eventsModel);
    }

    public class EventRepository: IEventRepository
    {
        public async Task AddEventsAsync(IEnumerable<Event> eventsModel)
        {
            using (var db = new LiteDatabase(@"MyData.db"))
            {
                // Get customer collection
                var events = db.GetCollection<Event>("Events");

                // Insert new customer document (Id will be auto-incremented)
                events.InsertBulk(eventsModel);
             
            }
        }
    }
}
