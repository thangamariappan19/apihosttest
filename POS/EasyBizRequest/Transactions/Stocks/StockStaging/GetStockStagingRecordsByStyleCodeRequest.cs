using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Transactions.Stocks.StockStaging
{
    [DataContract]
    [Serializable]
    public class GetStockStagingRecordsByStyleCodeRequest :BaseRequestType
    {
        [DataMember]
        public string StyleCode { get; set; }
    }
}
