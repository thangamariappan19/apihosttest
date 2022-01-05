using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizErrorLogger
{
    [DataContract]
    [Serializable]
    public class LogRequest
    {
        [DataMember]
        public string Source { get; set; }

        [DataMember]
        public string Message { get; set; }
    }
}
