using EasyBizDBTypes.Transactions.StockReturn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Transactions.Stocks.StockReturn
{
    [DataContract]
    [Serializable]
    public class UpdateStockReturnRequest : BaseRequestType
    {
        [DataMember]
        public StockReturnHeader StockReturnHeaderRecord { get; set; }
        [DataMember]
        public StockReturnDetails StockReturnDetailsList { get; set; }
        [DataMember]
        public int StoreID { get; set; }
        [DataMember]
        public string StoreCode { get; set; }
    }
}
