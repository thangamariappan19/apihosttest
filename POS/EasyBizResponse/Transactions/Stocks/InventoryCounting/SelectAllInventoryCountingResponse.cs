using EasyBizDBTypes.Transactions.Stocks.InventoryCounting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Transactions.Stocks.InventoryCounting
{
    [DataContract]
    [Serializable]
    public class SelectAllInventoryCountingResponse : BaseResponseType
    {
        public List<InventoryCountingHeader> InventoryCountingHeaderList { get; set; }
    }

    [DataContract]
    [Serializable]
    public class GetInventoryCountingInitResponse : BaseResponseType
    {
        public List<InventoryInit> InventoryInitList { get; set; }       
    }

    [DataContract]
    [Serializable]
    public class GetInventoryCountingInitRecordResponse : BaseResponseType
    {       
        public InventoryInit InventoryInitRecord { get; set; }
    }

    [DataContract]
    [Serializable]
    public class GetInventoryManualCountRecordResponse : BaseResponseType
    {
        public InventoryManualCount InventoryManualCountRecord { get; set; }
    }
}
