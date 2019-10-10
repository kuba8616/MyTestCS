using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBuildTest.Data.Model
{
    public class EventJson
    {
        public string Id { get; set; }
        public string State { get; set; }
        public string Type { get; set; }
        public string Host { get; set; }
        public long Timestamp { get; set; }
    }
}
