using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.TransactionLogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizDBTypes.Transactions.StockReturn
{
    [DataContract]
    [Serializable]
    public class StockReturnHeader : BaseType
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
        public int ToWareHouseID { get; set; }
        [DataMember]
        public int FromStoreID { get; set; }
        [DataMember]
        public string Status { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public string ToWareHouseCode { get; set; }
        [DataMember]
        public List<StockReturnDetails> StockReturnDetailsList { get; set; }
        [DataMember]
        public List<TransactionLog> TransactionLogList { get; set; }
        [DataMember]
        public string StoreCode { get; set; }
        [DataMember]
        public string ReturnType { get; set; }
        [DataMember]
        public string BinCode { get; set; }

    }
}
