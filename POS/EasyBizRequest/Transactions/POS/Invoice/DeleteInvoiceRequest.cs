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
    public class DeleteInvoiceRequest : BaseRequestType
    {
        public int ID { get; set; }
    }
}
