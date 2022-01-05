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
    public  class SelectAllCashInCashOutDateWiseRequest:BaseRequestType
    {
        [DataMember]
        public DateTime Date { get; set; }
        [DataMember]
        public int StoreID { get; set; }
        [DataMember]
        public string CategoryType { get; set; }
    }
}
