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
    public class SelectByNonTraddingDetailsIDRequest : BaseRequestType
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string Mode { get; set; }
        [DataMember]
        public string RefDocumentNo { get; set; }
    }
}
