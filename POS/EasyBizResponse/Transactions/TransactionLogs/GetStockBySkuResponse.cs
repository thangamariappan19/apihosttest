using EasyBizDBTypes.Masters;
using EasyBizDBTypes.Transactions.TransactionLogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Transactions.TransactionLogs
{
    [DataContract]
    [Serializable]
    public class GetStockBySkuResponse : BaseResponseType
    {
        [DataMember]
        public TransactionLog StockData { get; set; }
        [DataMember]
        public TransactionLog StockDataStockReturn { get; set; }

        public List<SKUMasterTypes> SKUMasterTypesList { get; set; }
        public List<TransactionLog> ScaleWiseStockList { get; set; }

    }
}
