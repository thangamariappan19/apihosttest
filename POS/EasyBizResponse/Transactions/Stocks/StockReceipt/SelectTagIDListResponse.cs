using EasyBizDBTypes.Transactions.Stocks.StockReceipt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Transactions.Stocks.StockReceipt
{
    [DataContract]
    [Serializable]
    public class SelectTagIDListResponse : BaseResponseType
    {
        [DataMember]
        public List<TagIdItemDetails> RFIDList { get; set; }
    }
}
