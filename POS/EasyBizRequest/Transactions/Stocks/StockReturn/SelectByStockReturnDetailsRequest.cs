using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Transactions.Stocks.StockReturn
{
    [DataContract]
    [Serializable]
    public class SelectByStockReturnDetailsRequest : BaseRequestType
    {
        [DataMember]
        public int ID { get; set; }
    }
}
