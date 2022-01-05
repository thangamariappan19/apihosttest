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
    public class SelectAllStockRequestRequest : BaseRequestType
    {
        public string Mode { get; set; }
        public string StoreCode { get; set; }
        [DataMember]
        public int StoreID { get; set; }
    }
}
