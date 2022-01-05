using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.DivisionMasterRequest
{   
    [DataContract]
    [Serializable]
   public class SelectByDivisionIDRequest : BaseRequestType
    {
        [DataMember]
        public int ID { get; set; }
    }
}
