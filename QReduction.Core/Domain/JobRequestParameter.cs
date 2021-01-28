using System;
using System.Collections.Generic;
using System.Text;

namespace QReduction.Core.Domain
{
    public class JobRequestParameter
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }

        public string ValueType { get; set; }

        public int JobRequestId { get; set; }

        public JobRequest JobRequest { get; set; }
    }
}
