using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizDBTypes.Transactions.Stocks.StockReceipt;
using EasyBizDBTypes.Transactions.TransactionLogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizDBTypes.Transactions.StockReceipt
{
    [DataContract]
    [Serializable]
    public class StockReceiptHeader : BaseType
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public int StockRequestID { get; set; }
        [DataMember]
        public string StockRequestDocumentNo { get; set; }
        [DataMember]
        public string DocumentNo { get; set; }
        [DataMember]
        public DateTime DocumentDate { get; set; }
        [DataMember]
        public DateTime CreateOn { get; set; }
        [DataMember]
        public DateTime UpdateOn { get; set; }
        [DataMember]
        public int TotalQuantity { get; set; }
        [DataMember]
        public bool Type { get; set; }
        [DataMember]
        public int TotalReceivedQuantity { get; set; }
        [DataMember]
        public string Status { get; set; }
        [DataMember]
        public string StoreName { get; set; }
        [DataMember]
        public string Remarks { get; set; }
         [DataMember]
        public bool WithOutBaseDoc { get; set; }
        [DataMember]
        public string FromWarehouseCode { get; set; }
        [DataMember]
        public string Fromwarehousename { get; set; }
        [DataMember]
        public int FromWareHouseID { get; set; }
        [DataMember]
        public int StoreID { get; set; }
        [DataMember]
        public bool fromApplication { get; set; }
        [DataMember]
        public List<StockReceiptDetails> StockReceiptDetailsList { get; set; }
        [DataMember]
        public List<TransactionLog> TransactionLogList { get; set; }
        [DataMember]
        public List<BinLogTypes> BinLogList { get; set; }
        [DataMember]
        public string StoreCode { get; set; }
        [DataMember]
        public string StockRequestStatus { get; set; }
        [DataMember]
        public string DataFrom { get; set; }
        [DataMember]
        public bool IsFlaged { get; set; }
        [DataMember]
        public string ReceivedType { get; set; }
        [DataMember]
        public int Discrepancies { get; set; }
        [DataMember]
        public List<TagIdItemDetails> RFIDList { get; set; }
    }
}

