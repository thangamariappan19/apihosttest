using EasyBizDBTypes.Transactions.Stocks.OpeningStock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Transactions.Stocks.OpeningStock
{
    [DataContract]
    [Serializable]
    public class SelectAllOpeningStockResponse : BaseResponseType
    {
        public List<OpeningStockHeader> OpeningStockHeaderList { get; set; }
    }
}
