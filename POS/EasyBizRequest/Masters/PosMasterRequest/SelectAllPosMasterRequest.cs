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
    public class SelectAllPosMasterRequest:BaseRequestType
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public int StoreID { get; set; }
    }
}
