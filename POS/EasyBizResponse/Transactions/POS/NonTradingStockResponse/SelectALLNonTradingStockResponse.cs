using EasyBizDBTypes.Transactions.Stocks.NonTradingItemStock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Transactions.POS.NonTradingStockResponse
{
    [DataContract]
    [Serializable]
    public class SelectALLNonTradingStockResponse : BaseResponseType
    {
        [DataMember]
        public List<NonTradingStockHeaderTypes> NonTradingStockHeaderList { get; set; }
        [DataMember]
        public List<NonTradingStockDetailsTypes> NonTradingStockDetailsList { get; set; }
    }

}
