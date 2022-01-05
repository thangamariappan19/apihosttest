using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Transactions.Promotions.PromotionMappingRequest
{
    public class SelectPromotionMapListForCategoryRequest : BaseRequestType
    {
        [DataMember]

        public long WNPromotionID { get; set; } 
    }
}
