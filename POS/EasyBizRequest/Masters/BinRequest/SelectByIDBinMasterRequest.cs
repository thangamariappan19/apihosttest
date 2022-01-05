using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.BinRequest
{
    [DataContract]
    [Serializable]
    public class SelectByIDBinMasterRequest : BaseRequestType
    {
        [DataMember]
        public int ID { get; set; }
    }
}
