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
    public class FindStockRequest : BaseRequestType
    {
        [DataMember]
        public string SearchString { get; set; }

        [DataMember]
        public int CountryID { get; set; }
    }
}
