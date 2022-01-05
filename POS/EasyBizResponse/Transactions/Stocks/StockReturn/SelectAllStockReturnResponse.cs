using EasyBizDBTypes.Transactions.StockReturn;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Transactions.Stocks.StockReturn
{
    [DataContract]
    [Serializable]
    public class SelectAllStockReturnResponse : BaseResponseType
    {
        [DataMember]
        public List<StockReturnHeader> StockReturnHeaderList { get; set; }
        [DataMember]
        public List<StockReturnDetails> StockReturnDetailsList { get; set; }
        

    }
}
