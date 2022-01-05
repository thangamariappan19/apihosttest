using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Transactions.Stocks.StockRequest
{
    [DataContract]
    [Serializable]
    public class SelectByStockRequestIDRequest : BaseRequestType
    {
        [DataMember]
        public int ID { get; set; }
    }
}
