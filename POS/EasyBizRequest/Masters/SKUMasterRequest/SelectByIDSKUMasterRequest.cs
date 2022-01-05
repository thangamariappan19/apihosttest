using EasyBizRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.SKUMasterRequest
{
    [Serializable]
    [DataContract]
    public class SelectByIDSKUMasterRequest : BaseRequestType
    {
        [DataMember]
        public int ID { get; set; }
        public String Source { get; set; }

        [DataMember]
        public String SkuCode { get; set; }
    }
}
