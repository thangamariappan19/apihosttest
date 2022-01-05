using EasyBizDBTypes.Transactions.StockStaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Transactions.Stocks.StockStaging
{
    [DataContract]
    [Serializable]
    public class GetStockStagingRecordsByStyleCodeResponse :BaseResponseType
    {
        [DataMember]
        public List<ItemStockStaging> ItemStockStagingList { get; set; }
    }
}
