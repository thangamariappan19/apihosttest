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
    public class SelectAllSalesExchangeDetailRequest : BaseRequestType
    {
        [DataMember]
        public long SalesExchangeID { get; set; }

        [DataMember]
        public string InvoiceNo { get; set; }

        [DataMember]
        public string Mode { get; set; }
    }
}
