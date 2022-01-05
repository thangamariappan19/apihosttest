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
    public class SelectByStockReceiptIDRequest : BaseRequestType
    {
         [DataMember]
        public int ID { get; set; }
         [DataMember]
         public string StockRequestDocumentNo { get; set; }
        [DataMember]
        public DateTime DocumentDate { get; set; }
        [DataMember]
        public DateTime ToDate { get; set; }
    }
}
