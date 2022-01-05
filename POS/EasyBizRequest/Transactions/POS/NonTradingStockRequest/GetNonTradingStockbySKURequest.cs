using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Transactions.POS.NonTradingStockRequest
{
    [DataContract]
    [Serializable]
    public class GetNonTradingStockBySKURequest:BaseRequestType
    {
        [DataMember]
        public string SKUCode { get; set; }

        [DataMember]
        public int StoreID { get; set; }
    }
}
