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
    public class SelectAllInventoryCountingRequest : BaseRequestType
    {
    }

    [DataContract]
    [Serializable]
    public class GetInventoryCountingInitRequest : BaseRequestType
    {

    }

    [DataContract]
    [Serializable]
    public class GetInventoryCountingInitRecordRequest : BaseRequestType
    {
        public string SelectionMode { get; set; }
        public DateTime DocumentDate { get; set; }
        public string DocumentNo { get; set; }
        public string GroupByMode { get; set; }
    }

    [DataContract]
    [Serializable]
    public class GetInventoryManualCountRecordRequest : BaseRequestType
    {
        public string DocumentNo { get; set; }
    }
}
