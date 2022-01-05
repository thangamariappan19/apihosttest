using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.TransactionLogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizDBTypes.Transactions.StockStaging
{
    [DataContract]
    [Serializable]
    public class StockAdjustmentHeader : BaseType
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string DocumentNumber { get; set; }
        [DataMember]
        public DateTime DocumentDate { get; set; }
        [DataMember]
        public int StyleID { get; set; }
        [DataMember]
        public string StyleCode { get; set; }
        [DataMember]
        public List<StockAdjustmentDetails> StockAdjustmentDetailList { get; set; }
        [DataMember]
        public int StoreID { get; set; }
        [DataMember]
        public string StoreCode { get; set; }
        public int CountryID { get; set; }
        [DataMember]
        public string CountryCode { get; set; }
        [DataMember]
        public List<TransactionLog> TransactionLogList { get; set; }  
        
    }
}
