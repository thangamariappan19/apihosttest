using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Transactions.PriceChange
{
    [DataContract]
    [Serializable]
    public class PriceUpdateRequest : BaseRequestType
    {
        public int StoreID { get; set; }
        public DateTime PriceChangeDate { get; set; }
    }
}
