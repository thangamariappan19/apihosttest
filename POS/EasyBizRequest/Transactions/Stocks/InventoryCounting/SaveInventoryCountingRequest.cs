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
    public class SaveInventoryCountingRequest : BaseRequestType
    {
        [DataMember]
        public InventoryCountingHeader InventoryCountingHeaderRecord { get; set; }
        public List<InventoryCountingDetails> InventoryCountingDetailsList { get; set; }
    }
}
