using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Transactions.Stocks.StockAdjustment
{
    [DataContract]
    [Serializable]
    public class GetAllStockAdjustmentRecordRequest :BaseRequestType
    {
        [DataMember]
        public int StoreID { get; set; }
    }
}
