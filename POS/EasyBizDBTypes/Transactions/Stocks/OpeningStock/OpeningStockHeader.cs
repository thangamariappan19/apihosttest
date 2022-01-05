using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.TransactionLogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizDBTypes.Transactions.Stocks.OpeningStock
{
    [DataContract]
    [Serializable]
    public class OpeningStockHeader : BaseType
    {
        [DataMember]
        public int ID { get; set; }        
        [DataMember]
        public string DocumentNo { get; set; }
        [DataMember]
        public DateTime DocumentDate { get; set; }
        [DataMember]
        public int TotalQuantity { get; set; }
        [DataMember]
        public bool Type { get; set; }            
        [DataMember]
        public string StoreName { get; set; }
        [DataMember]
        public string Remarks { get; set; }       
        [DataMember]
        public int StoreID { get; set; }
        [DataMember]
        public List<OpeningStockDetails> OpeningStockDetailsList { get; set; }
        [DataMember]
        public List<TransactionLog> TransactionLogList { get; set; }       
        [DataMember]
        public string StoreCode { get; set; }
        [DataMember]
        public int CreateBy { get; set; }
        [DataMember]
        public int CountryID { get; set; }
        [DataMember]
        public string CountryCode { get; set; }
    }
}
