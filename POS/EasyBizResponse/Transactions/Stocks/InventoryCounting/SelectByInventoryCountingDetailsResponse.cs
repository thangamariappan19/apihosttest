using EasyBizDBTypes.Transactions.Stocks.InventoryCounting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Transactions.Stocks.InventoryCounting
{
    [Serializable]
    [DataContract]
    public class SelectByInventoryCountingDetailsResponse : BaseResponseType
    {
        [DataMember]
        public List<InventoryCountingDetails> InventoryCountingDetailsRecord { get; set; }
    }
}
