using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.SKUMasterRequest
{
    [Serializable]
    [DataContract]
    public class SelectByALLSKUImagesRequest : BaseRequestType
    {
        [DataMember]
        public long SKUID { get; set; }

        [DataMember]
        public long StyleID { get; set; }
    }
}
