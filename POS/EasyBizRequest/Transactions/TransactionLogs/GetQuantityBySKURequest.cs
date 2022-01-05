using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Transactions.TransactionLogs
{
   public class GetQuantityBySKURequest : BaseRequestType
    {
        [DataMember]
        public string Department { get; set; }
        [DataMember]
        public string Productcode { get; set; }
        [DataMember]
        public string ColorCode { get; set; }
        [DataMember]
        public string StyleCode { get; set; }
        [DataMember]
        public string SizeCode { get; set; }
        [DataMember]
        public int StoreID { get; set; }
    }
}
