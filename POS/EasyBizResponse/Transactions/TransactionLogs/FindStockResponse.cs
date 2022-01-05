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
    public class FindStockResponse :BaseResponseType
    {
        [DataMember]
        public List<TransactionLog> StockList { get; set; }

        [DataMember]
        public DataSet StyleSummaryDataSet { get; set; }
    }
}
