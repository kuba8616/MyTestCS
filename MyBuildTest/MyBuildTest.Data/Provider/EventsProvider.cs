using MyBuildTest.Data.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace MyBuildTest.Data.Provider
{
    public interface IEventsProvider
    {
        Task<IEnumerable<EventJson>> GetEventsAsync();
    }

    public class EventsProvider : IEventsProvider
    {
        private readonly string path;

        public EventsProvider(string path)
        {
            this.path = path;
        }
        public async Task<IEnumerable<EventJson>> GetEventsAsync()
        {
            var events = new List<EventJson>();
            using (StreamReader sr = new StreamReader(path))
            {
                try
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        var @event = JsonConvert.DeserializeObject<EventJson>(line);
                        events.Add(@event);
                    }
                }
                catch(Exception ex)
                {

                }
               
            }

            return events;
        }
    }
}
