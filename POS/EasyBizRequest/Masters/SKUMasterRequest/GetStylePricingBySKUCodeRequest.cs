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
    public class GetStylePricingBySKUCodeRequest:BaseRequestType
    {
        [DataMember]
        public string SKUCode { get; set; }
        [DataMember]
        public string Department { get;set;}
        [DataMember]
        public string ProductCode { get; set; }
        [DataMember]
        public string ColorCode { get; set; }
        [DataMember]
        public string SizeCode { get; set; }
        [DataMember]        
      
        public int PriceListID { get; set; }
    }
}
