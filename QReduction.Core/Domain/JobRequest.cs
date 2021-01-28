using System;
using System.Collections.Generic;
using System.Text;

namespace QReduction.Core.Domain
{
    public class JobRequest
    {
        public JobRequest() => JobRequestParameters = new HashSet<JobRequestParameter>();

        public int Id { get; set; }

        public bool IsDone { get; set; }

        public int JobId { get; set; }

        public Job Job { get; set; }

        public ICollection<JobRequestParameter> JobRequestParameters { get; set; }

    }
}
