using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.CustomerSpecialPriceMasterRequest
{
    [Serializable]
    [DataContract]
    public class SelectByIDCustomerSpecialStoreDetailsRequest : BaseRequestType
    {
        [DataMember]
        public int ID { get; set; }


        [DataMember]
        public String Type { get; set; }
        [DataMember]
        public String DetailsType { get; set; }
    }
}
