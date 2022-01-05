using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.SKUMasterRequest
{
    [DataContract]
    [Serializable]
    public class SelectSKUByStyleIDRequest : BaseRequestType
    {
        [DataMember]
        public int StyleID { get; set; }
    }
}
