using EasyBizDBTypes.Transactions.TransactionLogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Transactions.POS.NonTradingStockResponse
{
    [DataContract]
    [Serializable]

    public class GetNonTradingStockBySKUResponse:BaseResponseType
    {
        [DataMember]
        public TransactionLog NonTradingStockData { get; set; }

        [DataMember]
        public List<TransactionLog> NonTradingStockList { get; set; }


    }
}
