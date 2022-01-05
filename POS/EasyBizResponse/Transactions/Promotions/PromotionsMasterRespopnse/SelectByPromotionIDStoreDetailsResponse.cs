using EasyBizDBTypes.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.PromotionsMasterResponse
{
    public class SelectByPromotionIDStoreDetailsResponse : BaseResponseType
    {
        [DataMember]
        public List<CommonUtil> DetailsRecord { get; set; }
       
       
        
    }
}
