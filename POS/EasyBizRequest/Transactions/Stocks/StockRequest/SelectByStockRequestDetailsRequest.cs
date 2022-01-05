using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Transactions.Stocks.StockRequest
{
    [DataContract]
    [Serializable]
    public class SelectByStockRequestDetailsRequest : BaseRequestType
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string StockRequestDocumentNo { get; set; }
        [DataMember]
        public string StoreCode { get; set; }
        [DataMember]
        public string Type { get; set; }
         [DataMember]
        public bool WithOutBaseDoc { get; set; }
    }
}
