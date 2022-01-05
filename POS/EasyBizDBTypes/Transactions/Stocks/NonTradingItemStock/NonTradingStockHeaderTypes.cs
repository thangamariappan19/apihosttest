using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.TransactionLogs;
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
    public class NonTradingStockHeaderTypes : BaseType
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string DocumentNo { get; set; }
        [DataMember]
        public DateTime DocumentDate { get; set; }
        [DataMember]
        public int CountryID { get; set; }
        [DataMember]
        public int StoreID { get; set; }
        [DataMember]
        public long EmployeeID { get; set; }
        [DataMember]
        public string EmployeeName { get; set; }
        [DataMember]
        public string EmployeeCode { get; set; }
        [DataMember]
        public int ReceivedQty { get; set; }
        [DataMember]
        public int ReturnQty { get; set; }
        [DataMember]
        public string ReceivedType { get; set; }
        [DataMember]
        public string TransactionType { get; set; }      
        /*[DataMember]
        public DateTime CreateOn { get; set; }
        [DataMember]
        public DateTime UpdateOn { get; set; }*/
        //[DataMember]
        //public DateTime UpdateOn { get; set; }
        /*[DataMember]
        public DateTime UpdateBy { get; set; }*/
        //[DataMember]
        //public int SKUID { get; set; }
        [DataMember]
        public string SKUCode { get; set; }
        [DataMember]
        public string BarCode { get; set; }
        [DataMember]
        public string StoreCode { get; set; }
        [DataMember]
        public List<NonTradingStockDetailsTypes> NonTradingStockDetailsList { get; set; }
        [DataMember]
        public List<TransactionLog> TransactionLogList { get; set; }
        [DataMember]
        public string RefDocumentNo { get; set; }
        [DataMember]
        public int DummySerialNo { get; set; }
        [DataMember]
        public int RunningNo { get; set; }
        [DataMember]
        public int DocumentNumberingID { get; set; }
        [DataMember]
        public int CreateBy { get; set; }

    }
}
