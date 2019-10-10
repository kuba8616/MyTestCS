using MyBuildTest.Data.Provider;
using MyBuildTest.Data.Repositories;
using MyBuildTest.Domain.Service;
using System;

namespace MyBuildTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("My build test for CS");

            if (args == null)
            {
                Console.WriteLine("args is null");
            }

            string path = args[0];
            var repository = new EventRepository();
            var provider = new EventsProvider(path);

            var eventService = new EventService(provider, repository);
            eventService.SaveAsync();
        }        
    }   
}
