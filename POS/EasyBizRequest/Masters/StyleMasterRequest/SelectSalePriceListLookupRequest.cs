using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.StyleMasterRequest
{
    [DataContract]
    [Serializable]
   public class SelectSalePriceListLookupRequest:BaseRequestType
    {
        [DataMember]
        public int SalePriceListID { get; set; }
        [DataMember]
        public string stylecode { get; set; }
        [DataMember]
        public string Category { get; set; }

        [DataMember]
        public string Type { get; set; }
    }
}
