using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QReduction.Apis.Infrastructure
{
    public class SwaggerParams : IParameter
    {
        public string Description { get; set; }

        public Dictionary<string, object> Extensions { get { return new Dictionary<string, object> { { "test", true } }; } }

        public string In { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public bool Required { get; set; }
    }
}
