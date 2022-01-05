using EasyBizDBTypes.Transactions.StockReceipt;
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
    public class UpdateStockReceiptRequest : BaseRequestType
    {
        [DataMember]
        public StockReceiptHeader StockReceiptHeaderRecord { get; set; }
        [DataMember]
        public StockReceiptDetails StockReceiptDetailsList { get; set; }
    }
}
