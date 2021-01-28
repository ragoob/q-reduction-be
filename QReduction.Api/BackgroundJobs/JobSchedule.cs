using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QReduction.Api.BackgroundJobs
{
    public class JobSchedule
    {
        public JobSchedule(Type jobType, int intervals, bool startImediatly, int startInterval)
        {
            JobType = jobType;
            Intervals = intervals;
            StartImediatly = startImediatly;
            StartInterval = startInterval;
        }

        public Type JobType { get; }
        public int Intervals { get; }
        public int StartInterval { get; set; }
        public bool StartImediatly { get; set; }
    }
}
