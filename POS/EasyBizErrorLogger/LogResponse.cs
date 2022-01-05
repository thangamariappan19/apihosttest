using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace EasyBizErrorLogger
{
    [DataContract]
    [Serializable]
    public class LogResponse
    {
        [DataMember]
        public LoggerEnums.LogOpStatusCode StatusCode { get; set; }

        [DataMember]
        public string DisplayMessage { get; set; }

        [DataMember]
        public string ExceptionMessage { get; set; }

        [DataMember]
        public string StackTrace { get; set; }
    }
}
