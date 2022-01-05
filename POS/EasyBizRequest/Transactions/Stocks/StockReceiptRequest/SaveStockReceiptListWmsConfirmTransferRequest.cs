using EasyBizDBTypes.Transactions.Stocks.StockReceipt;
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
    public class SaveStockReceiptListWmsConfirmTransferRequest : BaseRequestType
    {
        public List<int_stockreceipt> int_stockreceiptConfirmTransfer { get; set; }
    }
}
