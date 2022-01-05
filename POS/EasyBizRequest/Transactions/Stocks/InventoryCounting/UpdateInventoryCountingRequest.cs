using EasyBizDBTypes.Transactions.Stocks.InventoryCounting;
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
    public class UpdateInventoryCountingRequest : BaseRequestType
    {
        [DataMember]
        public InventoryCountingHeader InventoryCountingHeaderRecord { get; set; }
        [DataMember]
        public InventoryCountingDetails InventoryCountingDetailsList { get; set; }
    }
}
