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
    public class SelectAllStockReceiptRequest : BaseRequestType
    {
        [DataMember]
        public int StoreID { get; set; }
         [DataMember]    
        public DateTime BusinessDate { get; set; }
        [DataMember]
        public DateTime FromDate { get; set; }
        [DataMember]
        public DateTime ToDate { get; set; }
        [DataMember]
        public string StoreCode { get; set; }
    }
}
