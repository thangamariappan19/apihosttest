using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Transactions.Promotions.WNPromotionRequest
{
  public class SelectWNPromotionLookUpRequest : BaseRequestType
    {
       [DataMember]
       public int WNPromotionID { get; set; }
    }
}
