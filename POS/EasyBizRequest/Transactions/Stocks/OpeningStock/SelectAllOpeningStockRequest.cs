using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Transactions.Stocks.OpeningStock
{
    [DataContract]
    [Serializable]
    public class SelectAllOpeningStockRequest : BaseRequestType
    {
        [DataMember]
        public int StoreID { get; set; }
        [DataMember]
        public DateTime BusinessDate { get; set; }
    }
}
