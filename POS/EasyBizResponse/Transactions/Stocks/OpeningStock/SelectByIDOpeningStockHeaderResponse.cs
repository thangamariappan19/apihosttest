using EasyBizDBTypes.Transactions.Stocks.OpeningStock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Transactions.Stocks.OpeningStock
{
    [Serializable]
    [DataContract]
    public class SelectByIDOpeningStockHeaderResponse : BaseResponseType
    {
        [DataMember]
        public List<OpeningStockDetails> OpeningStockDetailsRecord { get; set; }
        public OpeningStockHeader OpeningStockHeaderRecord { get; set; }
    }
}
