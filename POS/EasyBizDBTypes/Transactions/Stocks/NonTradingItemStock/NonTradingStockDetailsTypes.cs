using EasyBizDBTypes.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizDBTypes.Transactions.Stocks.NonTradingItemStock
{
    [DataContract]
    [Serializable]
    public class NonTradingStockDetailsTypes : BaseType
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public int NonTradingHeaderID { get; set; }
        [DataMember]
        public string NonTradingHeaderDocumentNo { get; set; }
        [DataMember]
        public int SerialNo { get; set; }
        [DataMember]
        public int SKUID { get; set; }
        [DataMember]
        public string BarCode { get; set; }
        [DataMember]
        public string SKUCode { get; set; }
        [DataMember]  
        public int ReceivedQty { get; set; }
        [DataMember]
        public int ReturnQty { get; set; }
        [DataMember]
        public int StoreID { get; set; }

    }
}
