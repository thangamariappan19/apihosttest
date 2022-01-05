using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.PromotionsMasterRequest
{
    [DataContract]
    [Serializable]
    public class SavePromotionsRequest : BaseRequestType
    {
    [DataMember]
        public PromotionsMaster PromotionsRecord { get; set; }
         [DataMember]
    public List<CommonUtil> StoreTypeList { get; set; }
         [DataMember]
         public List<CommonUtil> CustomerTypeList { get; set; }
         [DataMember]
         public List<CommonUtil> ProductTypeList { get; set; }
         [DataMember]
         public List<CommonUtil> BuyItemTypeList { get; set; }
         [DataMember]
         public List<CommonUtil> GetItemTypeList { get; set; }
    }
}
