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
    public class GetSystemStockByStoreResponse :BaseResponseType
    {
        [DataMember]
        public List<InventorySysCount> InventorySysCountList { get; set; }
    }
}
