using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Transactions.Stocks.StockReceiptRequest
{
    [DataContract]
    [Serializable]
    public class SelectTagIDListRequest : BaseRequestType
    {
        [DataMember]
        public string DocumentNo { get; set; }
    }
}
