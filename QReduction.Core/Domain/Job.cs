using System;
using System.Collections.Generic;
using System.Text;

namespace QReduction.Core.Domain
{
    public class Job
    {
        public Job() => JobRequests = new HashSet<JobRequest>();

        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<JobRequest> JobRequests { get; set; }

    }
}
