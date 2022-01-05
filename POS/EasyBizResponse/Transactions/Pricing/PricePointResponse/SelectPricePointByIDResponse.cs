using EasyBizDBTypes.Transactions.Pricing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Transactions.Pricing.PricePointResponse
{
    [DataContract]
    [Serializable]
    public class SelectPricePointByIDResponse :BaseResponseType
    {
        [DataMember]
        public List<PricePoint> PricePointList { get; set; }
    }
}
