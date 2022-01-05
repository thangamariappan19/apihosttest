using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.EmployeeFingerPrintRequest
{
    [DataContract]
    [Serializable]
   public class SelectEmployeeFingerPrintByIDRequest : BaseRequestType
    {
        [DataMember]
        public long ID { get; set; }
    }
}
