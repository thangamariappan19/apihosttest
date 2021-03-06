using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Transactions.Pricing.PricePointRequest
{
    [DataContract]
    [Serializable]
    public class SelectPricePointByIDRequest :BaseRequestType
    {
        [DataMember]
        public int ID { get; set; }

        public string PricePointCode { get; set; }
    }
}
