using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Transactions.Stocks.StockRequest
{
    [Serializable]
    [DataContract]
    public class SelectWhareHouseLookUpRequest : BaseRequestType
    {
        [DataMember]
        public List<WarehouseMaster> WarehouseMasterList = new List<WarehouseMaster>();
        [DataMember]
        public int CountryID { get; set; }
    }
}
