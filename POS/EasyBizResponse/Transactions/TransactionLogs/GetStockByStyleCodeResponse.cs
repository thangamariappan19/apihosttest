using EasyBizDBTypes.Transactions.TransactionLogs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Transactions.TransactionLogs
{
    [DataContract]
    [Serializable]
    public class GetStockByStyleCodeResponse :BaseResponseType
    {
        [DataMember]
        public List<TransactionLog> StockList { get; set; }
        [DataMember]
        public List<TransactionLog> ColorWiseStockList { get; set; }
        [DataMember]
        public List<TransactionLog> ScaleWiseStockList { get; set; }
        [DataMember]
        public DataSet StockDataSet { get; set; }
    }
}
