using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QReduction.Api.Models
{
    public class FirebaseMessage
    {
        public string to { get; set; }
       
        public Notification notification { get; set; }
        public MessageData data { get; set; }
    }

    public class MessageData
    {
      public string QueueOrder { get; set; }
      public long EvalutionId { get; set; }
                   
    }
    public class Notification
    {
        public string Body { get; set; }

        public string Content { get; set; }
       public bool   content_available { get; set; }
       public string   priority { get; set; } // = "high",
        public string title { get; set; }


    }
}
