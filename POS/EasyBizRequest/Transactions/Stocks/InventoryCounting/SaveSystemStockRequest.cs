using EasyBizDBTypes.Transactions.Stocks.InventoryCounting;
using EasyBizDBTypes.Transactions.TransactionLogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Transactions.Stocks.InventoryCounting
{
    [DataContract]
    [Serializable]
    public class SaveSystemStockRequest :BaseRequestType
    {
        [DataMember]
        public InventoryInit InventoryManualCountRecord { get; set; }
        [DataMember]
        public int RunningNo { get; set; }
        [DataMember]
        public int DocumentNumberingID { get; set; }
    }

    [DataContract]
    [Serializable]
    public class SaveManualStockRequest : BaseRequestType
    {
        [DataMember]
        public InventoryManualCount InventoryManualCountRecord { get; set; }

        [DataMember]
        public string Status { get; set; }
    }

    [DataContract]
    [Serializable]
    public class InventoryFinalizeRequest :BaseRequestType
    {
        [DataMember]
        public string DocumentNo { get; set; }
        [DataMember]
        public string Status { get; set; }
        [DataMember]
        public string RARemarks { get; set; }
        [DataMember]
        public List<TransactionLog> TransactionLogList { get; set; }        
    }

    [DataContract]
    [Serializable]
    public class InventorySyncRequest : BaseRequestType
    {
        [DataMember]
        public string DocumentNo { get; set; }

        [DataMember]
        public DateTime DocumentDate { get; set; }

        [DataMember]
        public int StoreID { get; set; }

        [DataMember]
        public string Remarks { get; set; }

        [DataMember]
        public string RARemarks { get; set; }

        [DataMember]
        public string CountingType { get; set; }

        [DataMember]
        public List<InventorySysCount> InventorySysCountList { get; set; }

        [DataMember]
        public List<InventoryManualCountDetail> ManualCountList { get; set; }

        [DataMember]
        public List<TransactionLog> TransactionLogList { get; set; }
    }
}
