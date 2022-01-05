using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.SKUMasterRequest
{
    public class SelectByIDsSKUMasterRequest : BaseRequestType
    {
        [DataMember]
        public string IDs { get; set; }
        public String Source { get; set; }

        [DataMember]
        public String SkuCode { get; set; }
    }
}
