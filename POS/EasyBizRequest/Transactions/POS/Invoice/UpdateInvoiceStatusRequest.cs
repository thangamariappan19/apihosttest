using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Transactions.POS.Invoice
{
    [DataContract]
    [Serializable]
    public class UpdateInvoiceStatusRequest :BaseRequestType
    {
        [DataMember]
        public long InvoiceID { get; set; }

        [DataMember]
        public string Status { get; set; }

        [DataMember]
        public int StoreID { get; set; }
    }
}
