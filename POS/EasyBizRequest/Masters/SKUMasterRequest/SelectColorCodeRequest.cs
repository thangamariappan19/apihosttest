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
   public class SelectColorCodeRequest: BaseRequestType
    {
        [DataMember]
        public string Department { get; set; }
        [DataMember]
        public string Productcode { get; set; }
    }
}
