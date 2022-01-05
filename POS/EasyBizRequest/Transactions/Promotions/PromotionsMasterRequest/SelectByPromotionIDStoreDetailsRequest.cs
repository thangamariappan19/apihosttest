using EasyBizDBTypes.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.PromotionsMasterRequest
{
    [Serializable]
    [DataContract]
    public class SelectByPromotionIDStoreDetailsRequest : BaseRequestType
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public String Type { get; set; }
        [DataMember]
        public Enums.PromotionRecordType DetailsType { get; set; }
        
    }
}
