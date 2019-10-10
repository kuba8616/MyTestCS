using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBuildTest.Domain.Extensions
{
    public static class LongExtensions
    {
        public static long GetTotalDuration(this IEnumerable<long> timeSpans)
        {
            var total = timeSpans.Max();
            foreach (var timeSpan in timeSpans.OrderByDescending(x => x).Skip(1))
            {
                total -= timeSpan;
            }

            return total;
        }
    }
}
