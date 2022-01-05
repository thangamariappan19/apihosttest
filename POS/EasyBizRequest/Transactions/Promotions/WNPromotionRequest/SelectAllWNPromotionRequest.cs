using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Transactions.Promotions.WNPromotionRequest
{
    [DataContract]
    [Serializable]
    public class SelectAllWNPromotionRequest :BaseRequestType
    {
        [DataMember]
        public int PriceListID { get; set; }

        [DataMember]
        public int CountryID { get; set; }
    }
}
