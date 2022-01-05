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
    public class SelectByInventoryCountingIDResponse : BaseResponseType
    {
        public InventoryCountingHeader InventoryCountingHeaderRecord { get; set; }
    }
}
