using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Transactions.POSOperations
{
    [DataContract]
    [Serializable]
    public class SelectAllCashInCashoutReportRequest: BaseRequestType
    {
        [DataMember]
        public DateTime FromDate { get; set; }
        [DataMember]
        public DateTime ToDate { get; set; }
        [DataMember]
        public int StoreID { get; set; }
        [DataMember]
        public string CategoryType { get; set; }

    }
}
