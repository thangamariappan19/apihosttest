using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.PosMasterRequest
{
    [Serializable]
    [DataContract]  
    public class DeletePosMasterRequest:BaseRequestType
    {
        [DataMember]
        public long ID { get; set; }

        [DataMember]
        public int StoreID { get; set; }
    }
}
