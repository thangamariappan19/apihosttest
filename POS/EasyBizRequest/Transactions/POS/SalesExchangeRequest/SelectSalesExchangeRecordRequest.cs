using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Transactions.POS.SalesExchangeRequest
{
    [DataContract]
    [Serializable]
    public class SelectSalesExchangeRecordRequest :BaseRequestType
    {
        [DataMember]
        public int SalesExchangeID { get; set; }

        [DataMember]
        public string SalesExchangeDocumentNo { get; set; }
    }
}
