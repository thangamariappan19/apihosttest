using EasyBizDBTypes.Common;
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
    public class SelectByIDCustomerSpecialPriceMasterRequest : BaseRequestType
    {
        [DataMember]
        public int ID { get; set; }
          [DataMember]
        public String CustomerSpecialPriceMasterInfo { get; set; }
        [DataMember]
          public String Source { get; set; }
        [DataMember]
        public Enums.SpecialPriceRecordType DetailsType { get; set; }
    }
}
