using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Transactions.Stocks.StockRequest
{
    [Serializable]
    [DataContract]
    public class SelectWhareouseLookUpResponse : BaseResponseType
    {
        [DataMember]
        public List<WarehouseMaster> WarehouseMasterList { get; set; }
    }
}
