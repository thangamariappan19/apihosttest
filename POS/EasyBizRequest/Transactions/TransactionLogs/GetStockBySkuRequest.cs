using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Transactions.TransactionLogs
{
    [DataContract]
    [Serializable]
    public  class GetStockBySkuRequest :BaseRequestType
    {
        [DataMember]
        public string SKUCode { get; set; }

        [DataMember]
        public int StoreID { get; set; }
    }
}
