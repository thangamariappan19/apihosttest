using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Transactions.TransactionLogs
{
    [DataContract]
    [Serializable]
    public class GetStockByStyleCodeRequest : BaseRequestType
    {
        [DataMember]
        public string StyleCode { get; set; }

        [DataMember]
        public string StockWiseName { get; set; }  

        //[DataContract]
        public string SearchValue { get; set; }        
        
    }
}
