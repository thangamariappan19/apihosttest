using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizDBTypes.Transactions.TransactionLogs
{
    [DataContract]
    [Serializable]
    public class FindStockByCountry 
    {
        [DataMember]
        public string SKUCode { get; set; }
        [DataMember]
        public string BarCode { get; set; }
        [DataMember]
        public string StyleCode { get; set; } 
        [DataMember]
        public string ColorCode { get; set; }
        [DataMember]
        public string SizeCode { get; set; }
        [DataMember]
        public string StoreCode { get; set; }
        [DataMember]
        public string StoreName { get; set; }
        [DataMember]
        public int StockQty { get; set; }
        [DataMember]
        public decimal Price { get; set; }
    }
}
